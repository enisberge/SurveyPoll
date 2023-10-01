namespace SurveyPoll.WebUI.Models
{
    public class AddSurveyViewModel
    {
        public List<QuestionAnswerViewModel> Questions { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}
