using SurveryPoll.DataAccess.Entities;

namespace SurveyPoll.WebUI.Models
{
    public class SurveyViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public List<CorrectAnswer> CorrectAnswers { get; set; }
    }
}
