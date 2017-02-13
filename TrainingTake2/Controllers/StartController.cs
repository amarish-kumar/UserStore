using System.Web.Http;
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

        //[Route("api/start/get")]
        public string Get()
        {
            _repository.Find("vova", "123");

            return "hello world";
        }

        //public IEnumerable<string> Get()
        //{

        //}
    }
}