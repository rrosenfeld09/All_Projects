using Microsoft.AspNetCore.Mvc;
using System;

namespace TimeDisplay.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            DateTime currentTime = DateTime.Now;
            ViewBag.Time_1 = String.Format("{0: MMM d, yyyy}", currentTime);
            ViewBag.Time_2 = String.Format("{0: h:m tt}", currentTime);
            return View();
        }
    }
}