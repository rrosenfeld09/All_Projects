using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FitnessChallenge.Models;
using System.Linq;

namespace FitnessChallenge.Controllers
{
    public class MainController : Controller
    {

        public MyContext _context;

        public MainController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login_action")]
        public IActionResult Login(User submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.username == submittedUser.username).FirstOrDefault();
                if(returnedUser != null && submittedUser.password == returnedUser.password)
                {
                    return Json($"nice job, {submittedUser.username}");
                }
                return Json("nahhhhh");
            }
            return View("index");
        }
    }
}