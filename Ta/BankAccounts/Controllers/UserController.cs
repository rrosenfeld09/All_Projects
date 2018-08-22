using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BankAccounts.Controllers
{

    public class UserController : Controller
    {
        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register_user")]
        public IActionResult RegisterUser(User submittedUser)
        {
            if(ModelState.IsValid)
            {
                //create new user profile
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                submittedUser.password = Hasher.HashPassword(submittedUser, submittedUser.password);
                _context.Users.Add(submittedUser);
                _context.SaveChanges();

                User returnedUser = _context.Users.OrderByDescending(p => p.created_at).FirstOrDefault();
                Console.WriteLine(returnedUser.userid);
                HttpContext.Session.SetInt32("userid", returnedUser.userid);

                return RedirectToAction("HomePage", "Account", new {userid = returnedUser.userid});
            }
            return View("Index");
        }

    }
}