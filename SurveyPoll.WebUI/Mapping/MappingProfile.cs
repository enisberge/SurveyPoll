using AutoMapper;
using SurveryPoll.DataAccess.Entities;
using SurveyPoll.WebUI.Models;

namespace SurveyPoll.WebUI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionWithOptionsViewModel>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionViewModel>().ReverseMap();

        }
    }
}
