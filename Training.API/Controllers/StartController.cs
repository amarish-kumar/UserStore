using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Training.Identity.Services;
using Training.Services;
using TrainingTake2.Models;
using TrainingTake2.Services;

namespace Training.API.Controllers
{
    public class StartController : ApiController
    {
        private readonly IQueueService _queue;
        private readonly IAuthRepository _repository;

        public StartController(IAuthRepository repository, IQueueService queue)
        {
            _repository = repository;
            _queue = queue;
        }

        [Authorize]
        public string Get()
        {
            return _repository.GetAll().Count().ToString();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public IHttpActionResult POSTCreate(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("wrong user details");

            if (_repository.IsEmailUnique(user.Email))
                return BadRequest("email already taken");

            var command = new CreateUserCommand
            {
                FirstName = user.UserName,
                Surname = user.UserName
            };

            var message = JsonConvert.SerializeObject(command);
            _queue.SendMessage(message);
            return Ok("user created");
        }
    }
}