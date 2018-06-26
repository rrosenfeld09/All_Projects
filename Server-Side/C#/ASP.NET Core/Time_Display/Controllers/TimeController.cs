using Microsoft.AspNetCore.Mvc;
using System;

namespace Time_Display
{
    public class Time : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            DateTime currentTime = DateTime.Now;
            ViewBag.time_1 = String.Format("{0: MMM d, yyyy}", currentTime);
            ViewBag.time_2 = String.Format("{0: h:mm tt}", currentTime);

            return View();
        }
    }
}