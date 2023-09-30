using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using System.Drawing.Printing;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyPoll.WebUI.Controllers
{
    public class SurveyController : Controller
    {
        private readonly QuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public SurveyController(QuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestion(int SayfaNo, int pageSize = 6) {

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
