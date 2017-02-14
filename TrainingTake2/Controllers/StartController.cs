using System.Linq;
using System.Web.Http;
using Training.Identity;
using Training.Identity.Services;

namespace TrainingTake2.Controllers
{
    public class StartController : ApiController
    {
        private readonly IAuthRepository _repository;

        public StartController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public string Get()
        {
            return _repository.GetAll().Count().ToString();
        }

        public void Post()
        {
            _repository.Add(new ApplicationUser
            {
                UserName = "andrey"
            });
        }
    }
}