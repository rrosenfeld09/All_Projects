using System;
using Microsoft.AspNetCore.Mvc;
using Dojo_Survey.Models;

namespace Dojo_Survey
{
    public class Form : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("results")]
        public IActionResult Process_Results(User submittedUser)
        {
            // ViewBag.user = submittedUser;
            return View("Results", submittedUser);
        }
    }
}