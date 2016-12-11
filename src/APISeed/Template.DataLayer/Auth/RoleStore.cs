using Microsoft.AspNet.Identity.EntityFramework;

namespace Template.DataLayer.Auth
{
    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}
