using System;
using Microsoft.AspNetCore.Mvc;
using DojoSurvey.Models;

namespace DojoSurvey.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("user/create")]
        public IActionResult Create(User submittedUser)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User()
                {
                    Name = submittedUser.Name,
                    Location = submittedUser.Location,
                    Language = submittedUser.Language,
                    Comment = submittedUser.Comment
                };
                return RedirectToAction("Success", newUser);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success(User submittedUser)
        {
            return View(submittedUser);
        }
    }
}