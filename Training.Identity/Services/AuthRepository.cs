using System;
using System.Linq;

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
            throw new NotImplementedException();
        }

        public void Edit(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}