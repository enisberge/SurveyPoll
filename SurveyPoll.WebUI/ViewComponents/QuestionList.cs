using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveryPoll.DataAccess.Repositories;
using SurveyPoll.WebUI.Models;
using X.PagedList;

namespace SurveyPoll.WebUI.Components
{
    public class QuestionList : ViewComponent
    {

        private readonly QuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionList(QuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int pageSize = 6)
        {
            var questions = _questionRepository.GetAllQuestionsWithOptions();

            var questionWithOptions = _mapper.Map<List<QuestionListViewModel>>(questions);

            var pagedQuestions = questionWithOptions.ToPagedList(page, pageSize);

            return View(pagedQuestions);
        }
    }
}
