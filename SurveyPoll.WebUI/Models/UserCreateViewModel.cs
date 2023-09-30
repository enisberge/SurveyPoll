using System.ComponentModel.DataAnnotations;

namespace SurveyPoll.WebUI.Models
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Ad bilgisi boş geçilemez.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad bilgisi boş geçilemez.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-posta bilgisi boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola bilgisi boş geçilemez.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Parolanız en az 8 karakter içermelidir.")]
        [MaxLength(14, ErrorMessage = "Parolanız en fazla 14 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+*!=])(?=.*\d).*$",
    ErrorMessage = "Parola en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Parola tekrar bilgisi boş geçilemez.")]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        public int Status { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
