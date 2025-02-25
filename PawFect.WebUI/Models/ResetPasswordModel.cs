using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; } // Eşleşmeyi sağlamak için Identity'den gelen token
        [Required(ErrorMessage = "E-posta gereklidir.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
