using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly SurveyQuestionRepository _surveyQuestionRepository;
        private readonly SurveyResponseRepository _surveyResponseRepository;
        private readonly AnswerRepository _answerRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public SurveyController(QuestionRepository questionRepository, IMapper mapper, SurveyRepository surveyRepository, UserManager<AppUser> userManager, CorrectAnswerRepository correctAnswerRepository, IHttpContextAccessor httpContextAccessor, SurveyQuestionRepository surveyQuestionRepository, SurveyResponseRepository surveyResponseRepository, AnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _surveyRepository = surveyRepository;
            _correctAnswerRepository = correctAnswerRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyResponseRepository = surveyResponseRepository;
            _answerRepository = answerRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Kullanıcı adı ve soyadı alanlarını doldurun
                    ViewBag.FirstName = user.Name; // Özelleştirilmiş kullanıcı modelinizdeki ad alanı
                    ViewBag.LastName = user.Surname;   // Özelleştirilmiş kullanıcı modelinizdeki soyad alanı
                }
            }
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
        [HttpGet("Survey/SurveyViewscore/{surveyCode}")]
        public async Task<IActionResult> SurveyViewscore(string surveyCode, int? page)
        {
            int pageNumber = page ?? 1; // Sayfa numarasını belirle, varsayılan olarak 1
            int pageSize = 10; // Her sayfa için gösterilecek öğe sayısı

            List<UserScoreDto> UserScores = _surveyRepository.GetUserScoresBySurveyCode(surveyCode);

            IPagedList<UserScoreDto> pagedUserScores = UserScores.ToPagedList(pageNumber, pageSize);

            return View(pagedUserScores);
        }



        [HttpGet("Survey/MakeSurvey/{surveyCode}")]

        public async Task<IActionResult> MakeSurvey(string surveyCode)
        {
            var result = _surveyRepository.GetSurveyWithQuestionsByGuid(surveyCode);

            var viewModel = new SurveyViewModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Title = result.Title,
                Questions = result.SurveyQuestions.Select(sq => sq.Question).ToList(),
                CorrectAnswers = result.CorrectAnswers
            };

            ViewBag.SurveyCode = surveyCode;
            ViewBag.SurveyId = result.Id;
            return View(viewModel);

        }
        [HttpGet]
        public async Task<IActionResult> GetSurveyList()
        {
            var user = await _userManager.GetUserAsync(User);
            var values = _surveyRepository.GetSurveyListByUserId(user.Id);

            var surveyList=_mapper.Map<List<SurveyListModel>>(values);
            return Json(surveyList);  
        }
        public IActionResult SurveyList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SurveyResponse([FromBody] SurveyResponseModel model)
        {

            var surveyResponse = new SurveyResponse
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                SurveyCode = model.SurveyCode,
                CreatedDate = DateTime.Now,
                SurveyId = model.SurveyId
            };

            _surveyResponseRepository.Add(surveyResponse);

            var answerResponse = model.Answers
     .Select(answer => new Answer
     {
         QuestionId = answer.QuestionId,
         QuestionOptionId = answer.QuestionOptionId,
         SurveyResponseId = surveyResponse.Id
     })
     .ToList();
            _answerRepository.AddRange(answerResponse);

            return Json(new { isSuccess = true });

        }
        public IActionResult MakeSurvey()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddSurvey([FromBody] AddSurveyViewModel model)
        {
            var currentContext = _httpContextAccessor.HttpContext;
            var baseUrl = $"{currentContext.Request.Scheme}://{currentContext.Request.Host}{currentContext.Request.PathBase}";

            Guid guid = Guid.NewGuid();
            string SurveyCode = guid.ToString();
            var user = await _userManager.GetUserAsync(User);
            var survey = new Survey
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.Title,
                SurveyCode = SurveyCode,
                CreatedDate = DateTime.Now,
                UserId = user == null ? 0 : user.Id
            };
            _surveyRepository.Add(survey);

            var surveyQuestion = model.Questions
     .Select(question => new SurveyQuestion
     {
         QuestionId = question.QuestionId,
         SurveyId = survey.Id
     })
     .ToList();
            _surveyQuestionRepository.AddRange(surveyQuestion);

            var correctAnswers = model.Questions
     .Select(question => new CorrectAnswer
     {
         QuestionId = question.QuestionId,
         QuestionOptionId = question.QuestionOptionId,
         SurveyId = survey.Id
     })
     .ToList();

            _correctAnswerRepository.AddRange(correctAnswers);

            return Json(new
            {
                isSuccess = true,
                message = "Anket oluşturma başarılı !",
                data = baseUrl + "/Survey/MakeSurvey/" + SurveyCode
            });
        }
    }
}
