using System.Linq;

namespace Training.Identity.Services
{
    public interface IAuthRepository
    {
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser Add(ApplicationUser entity);
        void Delete(ApplicationUser entity);
        void Edit(ApplicationUser entity);
        void Save();
        void SetRole(string userId, Roles role);
        bool IsEmailUnique(string email);
    }
}