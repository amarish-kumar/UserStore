using System;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Training.Identity.Services
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private readonly IdentityContext _context;

        public AuthRepository(IdentityContext context)
        {
            _context = context;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser Add(ApplicationUser entity)
        {
            return _context.Users.Add(entity);
        }

        public void Delete(ApplicationUser entity)
        {
            _context.Users.Remove(entity);
        }

        public void Edit(ApplicationUser entity)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == entity.Id);
            _context.Users.Remove(userToUpdate);
            _context.Users.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SetRole(string userId, Roles role)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var roleToSet = _context.Roles.FirstOrDefault(r => r.Name == role.ToString());
            user?.Roles.Add(new IdentityUserRole {RoleId = roleToSet?.Id});
        }

        public bool IsEmailUnique(string email)
        {
            //todo: use string.compare() instead
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    public enum Roles
    {
        user = 0,
        admin = 1
    }
}