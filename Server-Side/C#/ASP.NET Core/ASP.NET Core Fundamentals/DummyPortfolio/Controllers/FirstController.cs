using Microsoft.AspNetCore.Mvc;

namespace Portfolio_1.Controllers
{
    public class FirstController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult Projects()
        {
            return View();
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [Route("{name}")]
        public string nameInsert(string name)
        {
            string message = $"Hello, {name}! You rock.";
            return message;
        }
        [HttpPost]
        [Route("sendingData")]
        public IActionResult BackToHome()
        {
            return RedirectToAction("Index");
        }

    }
}