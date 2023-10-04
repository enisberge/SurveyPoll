namespace SurveyPoll.WebUI.Models
{
    public class UserScoreDto
    {
        public string SurveyTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }

    }
}
