using PawFect.WebUI.Identity;
using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class AccountModel : ApplicationUser
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
