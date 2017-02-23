using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Newtonsoft.Json;
using Training.DAL.Interfaces.Interfaces;
using Training.DAL.Interfaces.Models;
using Training.Identity;
using Training.Identity.Services;
using Training.Services;
using TrainingTake2.Models;
using TrainingTake2.Services;
namespace Training.API.Controllers
{
    [EnableCors("http://localhost:3000", "*", "*")]
    public class MainController : ApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly IQueueService _queue;
        private readonly IUserRepository _repository;

        public MainController(IUserRepository repository, IQueueService queue, IAuthRepository authRepository)
        {
            _repository = repository;
            _queue = queue;
            _authRepository = authRepository;
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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("wrong user details");
            //}

            if (_authRepository.IsEmailUnique(user.Email))
            {
                return BadRequest("email already taken");
            }

            CreateIdentity(user);
            SendCreateUserCommand(user);

            return Ok("user created");
        }

        [HttpPost]
        [Authorize]
        [Route("GetUserById")]
        public string GetUserById(string id)
        {
            var user = _authRepository.GetUserById(id);
            return JsonConvert.SerializeObject(user);
        }
        
        private void CreateIdentity(UserModel user)
        {
            var identity = _authRepository.Add(new ApplicationUser
            {
                Email = user.Email,
                UserName = $"{user.FirstName} {user.Surname}",
                PasswordHash = user.Password
            });
            _authRepository.Save();
            _authRepository.SetRole(identity.Id, Roles.user);
            _authRepository.Save();
        }


        private void SendCreateUserCommand(UserModel user)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserModel, CreateUserCommand>(); });

            var mapper = config.CreateMapper();
            var command = mapper.Map<UserModel, CreateUserCommand>(user);

            var message = JsonConvert.SerializeObject(command);
            _queue.SendMessage(message);
        }
    }
}