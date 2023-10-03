using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveryPoll.DataAccess.Entities;
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
        private readonly SurveyRepository _surveyRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly CorrectAnswerRepository _correctAnswerRepository;

        private readonly IMapper _mapper;
        public SurveyController(QuestionRepository questionRepository, IMapper mapper, SurveyRepository surveyRepository, UserManager<AppUser> userManager, CorrectAnswerRepository correctAnswerRepository)
        {
            _questionRepository = questionRepository;
            _surveyRepository = surveyRepository;
            _correctAnswerRepository = correctAnswerRepository;
            _userManager = userManager;
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
        public IActionResult GetQuestion(int SayfaNo, int pageSize = 6)
        {

            var questions = _questionRepository.GetAllApprovedQuestions();//bütün onaylı sorular
            var questionWithOptionsViewModels = _mapper.Map<List<QuestionListViewModel>>(questions);

            // Sayfada görüntülenecek verileri seçin
            var startIndex = (SayfaNo - 1) * pageSize;
            var pageData = questionWithOptionsViewModels.Skip(startIndex).Take(pageSize).ToList();

            var totalItemCount = questionWithOptionsViewModels.Count;
            var totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);

            return Json(new { Data = pageData, TotalPages = totalPages });

        }
        [HttpPost]
        public async Task<IActionResult> AddSurvey([FromBody] AddSurveyViewModel model)
        {
            Guid guid = Guid.NewGuid();
            string SurveyCode = guid.ToString();
            var user = await _userManager.GetUserAsync(User);
            var survey = new Survey
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.Title,
                SurveyCode = SurveyCode,
                UserId = user == null ? 0 : user.Id
            };
            _surveyRepository.Add(survey);
            var correctAnswers = model.Questions
     .Select(question => new CorrectAnswer
     {
         QuestionId = question.QuestionId,
         QuestionOptionId = question.QuestionOptionId,
         SurveyId = survey.Id
     })
     .ToList();

            _correctAnswerRepository.AddRange(correctAnswers);

            return Json(new { isSuccess = true, message = "Anket oluşturma başarılı !",data= Url.Content("~/") + SurveyCode
        });
        }
    }
}
