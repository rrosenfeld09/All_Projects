using System;
using Microsoft.AspNetCore.Mvc;
using DojoSurvey.Models;

namespace DojoSurvey.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process_data")]
        public IActionResult SubmittedData(User submittedUser)
        {
            User newUser = new User()
            {
                Name = submittedUser.Name,
                Location = submittedUser.Location,
                Language = submittedUser.Language,
                Comment = submittedUser.Comment
            };
            return View(newUser);
        }
    }
}