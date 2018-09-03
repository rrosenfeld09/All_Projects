using Microsoft.AspNetCore.Mvc;
using FormSubmission.Models;

namespace FormSubmission.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("user/create")]
        public IActionResult Create(User submittedUser)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            } 
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}