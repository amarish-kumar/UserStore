using System.Linq;
using System.Web.Http;
using Training.Identity.Services;

namespace Training.API.Controllers
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
            //todo: add user
        }
    }
}