namespace SurveyPoll.WebUI.Models
{
    public class QuestionViewModel
    {
        public string QuestionText { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionOptionViewModel> QuestionOptions { get; set; }
    }
}
