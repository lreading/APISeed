using DataLayer.Auth;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
