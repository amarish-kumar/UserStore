using System.Web.Http;
using System.Web.Http.Cors;
using Training.DAL.Interfaces.Interfaces;
using TrainingTake2.Services;

namespace TrainingTake2.Controllers
{
    [EnableCors("http://localhost:3000", "*", "*")]
    public class AccountController : ApiController
    {
        public AccountController(IUserRepository repository, IQueueService queue)
        {
        }
    }
}