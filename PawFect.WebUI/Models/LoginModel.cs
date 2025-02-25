using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "E-posta gereklidir.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
                    ErrorMessage = "Şifre en az bir büyük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string Password { get; set; }

        // Şifre onayı
        [Required(ErrorMessage = "Lütfen şifrenizi onaylayınız.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre ve onay şifresi uyuşmamaktadır.")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}



