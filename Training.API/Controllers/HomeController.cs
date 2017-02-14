using System.Web.Mvc;
using Training.Identity.Services;

namespace Training.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _repo;


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}