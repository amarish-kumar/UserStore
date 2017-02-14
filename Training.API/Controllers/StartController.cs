using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Identity;
using Training.Identity.Services;
using TrainingTake2.Models;

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

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public async Task<IHttpActionResult> POSTCreate(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("wrong user details");

            if (_repository.IsEmailUnique(user.Email))
                return BadRequest("email already taken");


            var x = _repository.Add(new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.Password.GetHashCode().ToString()
            });

            _repository.Save();
            _repository.SetRole(x.Id, Roles.user);
            _repository.Save();
            return Ok();
        }

        [HttpGet]
        [Route("CheckEmail/{email}")]
        public bool CheckEmail(string email)
        {
            return _repository.IsEmailUnique(email);
        }
    }
}