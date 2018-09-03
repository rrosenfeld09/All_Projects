using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LoginRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginRegistration.Controllers
{
    public class UserController : Controller
    {
        public UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Register()
        {
            var sessionResult = HttpContext.Session.GetInt32("loggedUserId");
            if(sessionResult != null)
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var sessionResult = HttpContext.Session.GetInt32("loggedUserId");
            if(sessionResult != null)
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }

        [HttpPost("register_action")]
        public IActionResult RegisterAction(User submittedUser)
        {
            if(ModelState.IsValid)
            {
                List<User> ReturnedValues = _context.Users.Where(p => p.email == submittedUser.email).ToList();
                if(ReturnedValues.Count == 0)
                {

                    //hash password
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    submittedUser.password = Hasher.HashPassword(submittedUser, submittedUser.password);

                    //insert into db
                    _context.Add(submittedUser);
                    _context.SaveChanges();

                    //store users id in session
                    User returnedUser = _context.Users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                    HttpContext.Session.SetInt32("loggedUserId", returnedUser.id);

                    return RedirectToAction("HomePage");
                }
                else
                {
                    ViewBag.EmailError = "This email is already in use, please sign in";
                    return View("Register");
                }
            }
            return View("Register");
        }

        [HttpPost("login_action")]
        public IActionResult LoginAction(LoginUser submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.Users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                if(returnedUser == null)
                {
                    ViewBag.LoginError = "Incorrect email/password. Try again";
                    return View("Login");
                }
                else 
                {
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(returnedUser, returnedUser.password, submittedUser.password))
                    {
                        HttpContext.Session.SetInt32("loggedUserId", returnedUser.id);
                        return RedirectToAction("HomePage");
                    }
                    else
                    {
                        ViewBag.LoginError = "Incorrect email/password. Try again";
                        return View("Login");
                    }
                }
            }
            return View("Login");
        }

        [HttpGet("homepage")]
        public IActionResult HomePage()
        {
            var sessionResult = HttpContext.Session.GetInt32("loggedUserId");
            if(sessionResult == null)
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Console.WriteLine("SESSION CLEARED");
            return RedirectToAction("Login");
        }
    }
}