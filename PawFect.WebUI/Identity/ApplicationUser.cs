using Microsoft.AspNetCore.Identity;

namespace PawFect.WebUI.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
