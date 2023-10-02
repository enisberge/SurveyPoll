namespace SurveyPoll.WebUI.Models
{
    public class UpdateQuestionViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<QuestionOptionViewModel> QuestionOptions { get; set; }
    }
}
