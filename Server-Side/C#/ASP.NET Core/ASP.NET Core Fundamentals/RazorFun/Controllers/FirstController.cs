using Microsoft.AspNetCore.Mvc;

namespace RazorFun.Controllers
{
    public class FirstController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}