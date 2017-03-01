using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Training.Identity.Services
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public AuthRepository(IdentityContext context)
        {
            _context = context;
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser Add(ApplicationUser entity)
        {
            return _context.Users.Add(entity);
        }

        public IdentityResult Add(ApplicationUser user, string password)
        {
            return _manager.Create(user, password);
        }

        public void Delete(ApplicationUser entity)
        {
            _context.Users.Remove(entity);
        }

        public IdentityResult Update(ApplicationUser entity)
        {
            return _manager.Update(entity);
        }

        public void SetRole(string userId, Roles role)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var roleToSet = _context.Roles.FirstOrDefault(r => r.Name == role.ToString());
            user?.Roles.Add(new IdentityUserRole {RoleId = roleToSet?.Id});
        }

        public bool IsEmailUnique(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null;
        }

        public ApplicationUser FindUser(string userName, string password)
        {
            return _manager.Find(userName, password);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }

    public enum Roles
    {
        user,
        admin
    }
}