namespace SurveyPoll.WebUI.Models
{
    public class SurveyListModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string SurveyCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
