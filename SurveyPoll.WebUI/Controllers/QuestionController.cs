using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveryPoll.DataAccess.Entities;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyPoll.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionRepository _questionRepository;
        private readonly QuestionOptionRepository _questionOptionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public QuestionController(QuestionRepository questionRepository, IMapper mapper, QuestionOptionRepository questionOptionRepository, UserManager<AppUser> userManager)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _questionOptionRepository = questionOptionRepository;
            _userManager = userManager;
        }

        //public IActionResult Category()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{
        //    var categories = _categoryRepository.GetActiveCategories();
        //    return Json(categories);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        //{
        //    Dictionary<string, List<string>> errors;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Category category = new Category()
        //            {
        //                CreatedDate = DateTime.Now,
        //                IsDeleted = false,
        //                Name = model.Name,
        //            };

        //            _categoryRepository.Add(category);

        //            // Kategori ekleme işlemi başarılı olduysa 200 OK durumu döndürün.
        //            return Json(new { isValid = true, isSuccess = true, message = "Kayıt işlemi başarılı." });
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { isValid = true, isSuccess = false, message = "Kategori eklenirken bir hata oluştu: " + ex.Message });
        //        }
        //    }

        //    errors = ModelState.ToDictionary(
        //    kvp => kvp.Key,
        //    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
        //    );
        //    return Json(new { isValid = false, errors = errors });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCategoryById(int id)
        //{
        //    Dictionary<string, List<string>> errors;
        //    var category = _categoryRepository.GetById(id);
        //    return Json(category);
        //}


        //[HttpPut]
        //public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel model)
        //{
        //    Dictionary<string, List<string>> errors;
        //    if (ModelState.IsValid)
        //    {
        //        var existingCategory = _categoryRepository.GetById(model.Id);

        //        if (existingCategory != null)
        //        {
        //            existingCategory.Name = model.Name;

        //        }
        //        _categoryRepository.Update(existingCategory);

        //        return Json(new { isValid = true, isSuccess = true, message = "Güncelleme işlemi başarılı." });
        //    }
        //    errors = ModelState.ToDictionary(
        //              kvp => kvp.Key,
        //              kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
        //              );
        //    return Json(new { isValid = false, errors = errors });
        //}
        //[HttpPut]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{

        //    var existingCategory = _categoryRepository.GetById(id);

        //    if (existingCategory != null)
        //    {
        //        existingCategory.IsDeleted = true;

        //    }
        //    _categoryRepository.Update(existingCategory);

        //    return Json(new { isSuccess = true});

        //}
        [Authorize]
        public async Task<IActionResult> List()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);
                var question = new Question
                {
                    QuestionText = model.QuestionText,
                    IsDeleted = model.IsDeleted,
                    Status = User.IsInRole("Admin") ? 2 : 1,
                    CreatedDate = model.CreatedDate,
                    AppUserId = Convert.ToInt32(user.Id)

                };
                _questionRepository.Add(question);
                var options = model.QuestionOptions.Select(optionViewModel => new QuestionOption
                {
                    OptionText = optionViewModel.OptionText,
                    CreatedDate = optionViewModel.CreatedDate,
                    QuestionId = question.Id // Soruyla ilişkilendirin
                }).ToList();
                _questionOptionRepository.AddRange(options);
                return Json(new { isSuccess = true, message = "Ekleme işlemi başarılı !" });
            }

            return Json(new { isSuccess = false, message = "Ekleme işlemi başarısız !" });
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion(int SayfaNo, int pageSize = 6)
        {
            var questions = new List<Question>();

            if (User.IsInRole("Admin"))
            {
            questions = _questionRepository.GetAllQuestionsWithOptions();
            }
            else if (User.IsInRole("User"))
            {
                var user = await _userManager.GetUserAsync(User);
             questions = _questionRepository.GetQuestionsForUser(user.Id);
            }

            var questionWithOptionsViewModels = _mapper.Map<List<QuestionListViewModel>>(questions);

            // Sayfada görüntülenecek verileri seçin
            var startIndex = (SayfaNo - 1) * pageSize;
            var pageData = questionWithOptionsViewModels.Skip(startIndex).Take(pageSize).ToList();

            var totalItemCount = questionWithOptionsViewModels.Count;
            var totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);

            return Json(new { Data = pageData, TotalPages = totalPages });

        }
        [HttpGet]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = _questionRepository.GetQuestionWithQuestionOptionsById(id);
            var questionViewModel = _mapper.Map<QuestionViewModel>(question);
            return Json(questionViewModel);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionViewModel model)
        {
            if (model != null)
            {
                // Modeldeki verileri kullanarak güncelleme işlemini gerçekleştirin
                var existingQuestion = _questionRepository.GetById(model.Id);

                if (existingQuestion != null)
                {
                    existingQuestion.QuestionText = model.QuestionText;

                    // QuestionOptions verilerini güncelleme işlemi
                    foreach (var option in model.QuestionOptions)
                    {
                        var existingOption = _questionOptionRepository.GetById(option.Id);
                        if (existingOption != null)
                        {
                            existingOption.OptionText = option.OptionText;
                            // Eksik olan işlemleri burada yapabilirsiniz, örneğin yeni seçenek eklemek ya da silmek
                        }
                    }

                    // Değişiklikleri veritabanına kaydedin
                    _questionRepository.Update(existingQuestion);

                    return Json(new { isSuccess = true, message = "Güncelleme başarılı !" });
                }
            }

            return Json(new { isSuccess = false, message = "Güncelleme başarısız !" });
        }
    }

}
