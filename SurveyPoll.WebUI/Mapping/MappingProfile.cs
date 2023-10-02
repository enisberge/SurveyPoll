using AutoMapper;
using Data.Entities;
using SurveryPoll.DataAccess.Entities;
using SurveyPoll.WebUI.Models;

namespace SurveyPoll.WebUI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionListViewModel>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionListViewModel>().ReverseMap();
            CreateMap<AppUser,AppUserViewModel >().ReverseMap();
            CreateMap<Question,QuestionViewModel>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionViewModel>().ReverseMap();



        }
    }
}
