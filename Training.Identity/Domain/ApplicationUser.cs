using Microsoft.AspNet.Identity.EntityFramework;

namespace Training.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfileId { get; set; }
    }
}