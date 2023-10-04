namespace SurveyPoll.WebUI.Models
{
    public class SurveyResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurveyCode { get; set; }
        public List<SelectedOptionModel> SelectedOptions { get; set; }
    }
}
