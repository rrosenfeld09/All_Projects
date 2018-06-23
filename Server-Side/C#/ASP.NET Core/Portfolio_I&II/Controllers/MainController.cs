using Microsoft.AspNetCore.Mvc;
namespace PortfolioI
{
    public class MainController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Main");
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult Projects()
        {
            return View("Project");
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult Contact()
        {
            return View("Contact");
        }
    }
}