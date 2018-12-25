using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FitnessChallenge.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FitnessChallenge.Controllers
{
    public class HomePageController : BaseEntity
    {
        public MyContext _context;

        public HomePageController(MyContext context)
        {
            _context = context;
        }
        
        [HttpGet("homepage")]
        public IActionResult HomePage()
        {
            if(IsUserInSession())
            {
                User returnedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                return View(returnedUser);
            }

            return RedirectToAction("Index", "User");
        }
    }
}