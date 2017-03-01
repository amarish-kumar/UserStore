using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Training.Identity.Domain;

namespace Training.Identity.Services
{
    public interface IAuthRepository
    {
        IdentityResult Add(ApplicationUser user, string password);
        ApplicationUser GetUserById(string id);
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser Add(ApplicationUser entity);
        void Delete(ApplicationUser entity);
        IdentityResult Update(ApplicationUser entity);
        void SetRole(string userId, Roles role);
        bool IsEmailUnique(string email);
        ApplicationUser FindUser(string userName, string password);
        IList<string> GetUserRoles(string userId);
    }
}