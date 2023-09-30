using System.ComponentModel.DataAnnotations;

namespace SurveyPoll.WebUI.Models
{
    public class UpdateCategoryViewModel
    {
        public int Id{ get; set; }
        [Required(ErrorMessage="Kategori adı boş geçilemez")] 
        public string Name { get; set; }
    }
}
