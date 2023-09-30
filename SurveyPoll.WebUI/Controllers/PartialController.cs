using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using System.Drawing.Printing;
using X.PagedList;

namespace SurveyPoll.WebUI.Controllers
{
    public class PartialController : Controller
    {
        private readonly QuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public PartialController(QuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public ActionResult LoadPagedQuestions(int page=1)
        {
            int pageSize = 6;
            var questions = _questionRepository.GetAllQuestionsWithOptions();
            var questionWithOptions = _mapper.Map<List<QuestionWithOptionsViewModel>>(questions);
            var pagedQuestions = questionWithOptions.ToPagedList(page, pageSize);

            return PartialView("LoadPagedQuestions",pagedQuestions); // _PagedQuestionsPartialView, sayfalamayı görüntüleyeceğiniz kısmın adı olmalıdır
        }

    }
}
