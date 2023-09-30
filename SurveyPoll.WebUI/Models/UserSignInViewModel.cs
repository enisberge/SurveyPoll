using System.ComponentModel.DataAnnotations;

namespace SurveyPoll.WebUI.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "E-posta bilgisi boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola bilgisi boş geçilemez.")]
        public string Password { get; set; }

    }
}
