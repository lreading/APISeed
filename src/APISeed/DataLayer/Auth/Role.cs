using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Auth
{
    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }
}
