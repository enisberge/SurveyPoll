using Microsoft.AspNetCore.Mvc;
using SurveryPoll.DataAccess.Entities;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyPoll.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly QuestionRepository _questionRepository;
        public QuestionController(CategoryRepository categoryRepository, QuestionRepository questionRepository)
        {
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
        }

        public IActionResult Category()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _categoryRepository.GetActiveCategories();
            return Json(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            Dictionary<string, List<string>> errors;
            if (ModelState.IsValid)
            {
                try
                {
                    Category category = new Category()
                    {
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        Name = model.Name,
                    };

                    _categoryRepository.Add(category);

                    // Kategori ekleme işlemi başarılı olduysa 200 OK durumu döndürün.
                    return Json(new { isValid = true, isSuccess = true, message = "Kayıt işlemi başarılı." });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = true, isSuccess = false, message = "Kategori eklenirken bir hata oluştu: " + ex.Message });
                }
            }

            errors = ModelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );
            return Json(new { isValid = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Dictionary<string, List<string>> errors;
            var category = _categoryRepository.GetById(id);
            return Json(category);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel model)
        {
            Dictionary<string, List<string>> errors;
            if (ModelState.IsValid)
            {
                var existingCategory = _categoryRepository.GetById(model.Id);

                if (existingCategory != null)
                {
                    existingCategory.Name = model.Name;

                }
                _categoryRepository.Update(existingCategory);

                return Json(new { isValid = true, isSuccess = true, message = "Güncelleme işlemi başarılı." });
            }
            errors = ModelState.ToDictionary(
                      kvp => kvp.Key,
                      kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                      );
            return Json(new { isValid = false, errors = errors });
        }
        [HttpPut]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           
            var existingCategory = _categoryRepository.GetById(id);

            if (existingCategory != null)
            {
                existingCategory.IsDeleted = true;

            }
            _categoryRepository.Update(existingCategory);

            return Json(new { isSuccess = true});

        }


        

    }

}
