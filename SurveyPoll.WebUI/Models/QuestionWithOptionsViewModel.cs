using SurveryPoll.DataAccess.Entities;

namespace SurveyPoll.WebUI.Models
{
    public class QuestionWithOptionsViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionOptionViewModel> QuestionOptions { get; set; }
    }
}
