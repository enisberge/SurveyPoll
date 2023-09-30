using System.ComponentModel.DataAnnotations;

namespace SurveyPoll.WebUI.Models
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage="Kategori adı boş geçilemez")] 
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
