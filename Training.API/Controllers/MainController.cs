using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using Training.DAL.Interfaces.Interfaces;
using Training.DAL.Interfaces.Models;
using Training.Identity;
using Training.Identity.Domain;
using Training.Identity.Services;
using Training.Services;
using TrainingTake2.Models;
using TrainingTake2.Services;

namespace Training.API.Controllers
{
    [RoutePrefix("api/v1")]
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
        [Authorize(Roles = "admin")]
        public IEnumerable<User> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public User Get(string id)
        {
            return _repository.FindOneBy(user => user.IdentityId.Equals(id));
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

            var appUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };
            var res = _authRepository.Add(appUser, user.Password);
            if (!res.Succeeded)
            {
                return BadRequest(res.Errors.ToList()[0]);
            }

            var identity = _authRepository.FindUser(user.Email, user.Password);
            _authRepository.SetRole(identity.Id, Roles.user);
            EmitCommand(user, Operation.Create, identity.Id);
            return Ok(identity.Id);
        }

        [HttpPut]
        [Authorize]
        [Route("Update")]
        public IHttpActionResult PUTUpdateUser(UserModel user)
        {
            EmitCommand(user, Operation.Update);
            return Ok("user updated");
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