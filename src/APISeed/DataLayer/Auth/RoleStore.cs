using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Auth
{
    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}
