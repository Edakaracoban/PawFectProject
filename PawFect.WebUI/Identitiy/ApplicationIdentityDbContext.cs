using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PawFect.WebUI.Identitiy
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser> // Identitynin dbcontextini oluşturduk.
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }
    }
}
