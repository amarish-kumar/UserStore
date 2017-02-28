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

        [HttpGet]
        [Route("get")]
        [Authorize]
        public IEnumerable<User> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public User Get(string id)
        {
            return _repository.FindOneBy(user => user.IdentityId == id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public IHttpActionResult POSTCreateUser(UserModel user)
        {
            if (_authRepository.IsEmailUnique(user.Email))
            {
                return BadRequest("email already taken");
            }

            var id = CreateIdentity(user);
            EmitCommand(user, Operation.Create, id);
            return Ok(id);
        }

        [HttpPut]
        [Authorize]
        [Route("Update")]
        public IHttpActionResult PUTUpdateUser(UserModel user)
        {
            _authRepository.Edit(new ApplicationUser
            {
                Email = user.Email,
                UserName = $"{user.FirstName} {user.Surname}",
                PasswordHash = user.Password,
                Id = user.IdentityId
            });
            EmitCommand(user, Operation.Update);
            return Ok("user updated");
        }

        private string CreateIdentity(UserModel user)
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
            return identity.Id;
        }

        private void EmitCommand(UserModel user, Operation operation, string identityId = "")
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserModel, Command>(); });
            var mapper = config.CreateMapper();

            if (operation == Operation.Create)
            {
                user.IdentityId = identityId;
            }

            var command = mapper.Map<UserModel, Command>(user);
            command.Operation = operation;
            var message = JsonConvert.SerializeObject(command);
            _queue.SendMessage(message);
        }
    }
}