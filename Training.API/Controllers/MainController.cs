using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Newtonsoft.Json;
using Training.DAL.Interfaces.Interfaces;
using Training.DAL.Interfaces.Models;
using Training.Services;
using TrainingTake2.Models;
using TrainingTake2.Services;

namespace Training.API.Controllers
{
    [EnableCors("http://localhost:3000", "*", "*")]
    public class MainController : ApiController
    {
        private readonly IQueueService _queue;
        private readonly IUserRepository _repository;

        public MainController(IUserRepository repository, IQueueService queue)
        {
            _repository = repository;
            _queue = queue;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet]
        [Route("GetUsers")]
        //[Authorize]
        public IEnumerable<User> Get()
        {
            //todo: change it to User
            return _repository.GetAll();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public IHttpActionResult POSTCreateUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("wrong user details");

            //if (_repository.IsEmailUnique(user.Email))
            //    return BadRequest("email already taken");

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserModel, CreateUserCommand>(); });

            var mapper = config.CreateMapper();
            var command = mapper.Map<UserModel, CreateUserCommand>(user);

            var message = JsonConvert.SerializeObject(command);
            _queue.SendMessage(message);
            return Ok("user created");
        }
    }
}