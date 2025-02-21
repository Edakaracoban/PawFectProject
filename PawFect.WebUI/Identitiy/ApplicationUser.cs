using Microsoft.AspNetCore.Identity;

namespace PawFect.WebUI.Identitiy
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
