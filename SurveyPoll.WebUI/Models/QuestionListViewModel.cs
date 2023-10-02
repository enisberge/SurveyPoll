using SurveryPoll.DataAccess.Entities;

namespace SurveyPoll.WebUI.Models
{
    public class QuestionListViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionOptionListViewModel> QuestionOptions { get; set; }
        public AppUserViewModel AppUser { get; set; }
    }
}
