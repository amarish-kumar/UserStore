namespace Training.Identity.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IdentityContext _context;

        public AuthRepository()
        {
            _context = new IdentityContext();
        }

        public ApplicationUser Find(string userName, string password)
        {
            var user = _context.Users.Find(userName, password);
            return user;
        }
    }
}