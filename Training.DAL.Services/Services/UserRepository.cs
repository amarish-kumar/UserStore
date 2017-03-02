using Training.DAL.Interfaces.Interfaces;
using Training.DAL.Interfaces.Models;
using Training.DAL.Services.Services;

namespace Training.DAL.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context, "users")
        {
        }
    }
}