using System;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Process_Results(string name, string location, string language, string comment)
        {
            ViewBag.name = name;
            ViewBag.location = location;
            ViewBag.language = language;
            ViewBag.comment = comment;
            return View("Results");
        }
    }
}