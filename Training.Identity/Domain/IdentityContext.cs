using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Training.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext()
            : base("AuthContext", false)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<IdentityContext>());
        }
    }
}