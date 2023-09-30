namespace SurveyPoll.WebUI.Models
{
    public class QuestionOptionViewModel
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreateDate { get; set; }
        public int QuestionId { get; set; }
    }
}
