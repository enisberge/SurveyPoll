using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveryPoll.DataAccess.Entities;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyPoll.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionRepository _questionRepository;
        private readonly QuestionOptionRepository _questionOptionRepository;

        private readonly IMapper _mapper;
        public QuestionController(QuestionRepository questionRepository, IMapper mapper, QuestionOptionRepository questionOptionRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _questionOptionRepository = questionOptionRepository;
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

        public async Task<IActionResult> List()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    QuestionText = model.QuestionText,
                    IsDeleted = model.IsDeleted,
                    Status = 2,
                    CreatedDate = model.CreatedDate
                };
                _questionRepository.Add(question);
                var options = model.QuestionOptions.Select(optionViewModel => new QuestionOption
                {
                    OptionText = optionViewModel.OptionText,
                    CreatedDate = optionViewModel.CreatedDate,
                    QuestionId = question.Id // Soruyla ilişkilendirin
                }).ToList();
                _questionOptionRepository.AddRange(options);
                return Ok("Soru ve seçenekler başarıyla kaydedildi.");
            }

            return BadRequest("Geçersiz veri girişi.");
        }

        [HttpGet]
        public IActionResult GetQuestion(int SayfaNo, int pageSize = 6)
        {

            var questions = _questionRepository.GetAllQuestionsWithOptions();
            var questionWithOptionsViewModels = _mapper.Map<List<QuestionWithOptionsViewModel>>(questions);

            // Sayfada görüntülenecek verileri seçin
            var startIndex = (SayfaNo - 1) * pageSize;
            var pageData = questionWithOptionsViewModels.Skip(startIndex).Take(pageSize).ToList();

            var totalItemCount = questionWithOptionsViewModels.Count;
            var totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);

            return Json(new { Data = pageData, TotalPages = totalPages });

        }
    }

}
