using System.Linq;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using Training.Identity.Services;
using Training.Services;
using TrainingTake2.Models;
using TrainingTake2.Services;

namespace Training.API.Controllers
{
    public class MainController : ApiController
    {
        private readonly IQueueService _queue;
        private readonly IAuthRepository _repository;

        public MainController(IAuthRepository repository, IQueueService queue)
        {
            _repository = repository;
            _queue = queue;
        }

        [Authorize(Roles = "admin")]
        public string Get()
        {
            return _repository.GetAll().Count().ToString();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public IHttpActionResult POSTCreateUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("wrong user details");

            if (_repository.IsEmailUnique(user.Email))
                return BadRequest("email already taken");

            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserModel, CreateUserCommand>(); });

            //var mapper = config.CreateMapper();
            //var command = mapper.Map<UserModel, CreateUserCommand>(user);

            //var command = Mapper.Map(user, new CreateUserCommand());

            var command = new CreateUserCommand
            {
                FirstName = user.FirstName,
            };

            var message = JsonConvert.SerializeObject(command);
            _queue.SendMessage(message);
            return Ok("user created");
        }
    }
}