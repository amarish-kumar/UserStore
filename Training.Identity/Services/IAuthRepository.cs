namespace Training.Identity.Services
{
    public interface IAuthRepository
    {
        ApplicationUser Find(string userName, string password);
    }
}