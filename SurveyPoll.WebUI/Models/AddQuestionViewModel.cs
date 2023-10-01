
using SurveryPoll.DataAccess.Entities;

namespace SurveyPoll.WebUI.Models
{
    public class AddQuestionViewModel
    {
        public string QuestionText { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public List<AddQuestionOptionViewModel> QuestionOptions { get; set; }
    }
}
