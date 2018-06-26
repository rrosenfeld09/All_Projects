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
    }
}