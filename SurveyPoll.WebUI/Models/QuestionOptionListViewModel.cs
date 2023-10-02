namespace SurveyPoll.WebUI.Models
{
    public class QuestionOptionListViewModel
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QuestionId { get; set; }
    }
}
