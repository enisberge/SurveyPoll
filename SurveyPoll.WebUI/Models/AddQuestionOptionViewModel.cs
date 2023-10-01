namespace SurveyPoll.WebUI.Models
{
    public class AddQuestionOptionViewModel
    {
        public string OptionText { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public int QuestionId { get; set; }
    }
}
