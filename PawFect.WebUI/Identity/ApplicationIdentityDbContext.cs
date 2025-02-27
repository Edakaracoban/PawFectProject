using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PawFect.WebUI.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser> //Identitynin dbcontextini oluşturduk.
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }
    }
    //Bu yapı, uygulamanızda kullanıcıların kimlik doğrulaması ve yetkilendirilmesi için temel olan ASP.NET Core Identity'yi kullanmanıza olanak tanır.
}
