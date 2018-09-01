using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
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
            RegLoginViewModel model = new RegLoginViewModel()
            {
                regUser = null,
                loginUser = null
            };
            return View(model);
        }

        [HttpPost("create_user")]
        public IActionResult CreateUser(RegLoginViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.users.Any(p => p.email == submittedUser.regUser.email))
                {
                    TempData["Error"] = "Already registered, please login";
                    return View("Index");
                }
                if(submittedUser.regUser.password == submittedUser.regUser.confirm_pw)
                {
                    //hash password
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    submittedUser.regUser.password = Hasher.HashPassword(submittedUser.regUser, submittedUser.regUser.password);

                    //save to db
                    _context.Add(submittedUser.regUser);
                    _context.SaveChanges();

                    User returnedUser = _context.users.Where(p => p.email == submittedUser.regUser.email).FirstOrDefault();
                    HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);   
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["Error"] = "Passwords don't match";
                    return View("Index");
                }
            }
            RegLoginViewModel lrmodel = new RegLoginViewModel()
            {
                regUser = submittedUser.regUser
            };
            return View("Index", lrmodel);
        }

        [HttpPost("login")]
        public IActionResult Login(RegLoginViewModel submittedUser)
        {
            RegLoginViewModel lrmodel = new RegLoginViewModel()
            {
                loginUser = submittedUser.loginUser
            };
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.email == submittedUser.loginUser.email).FirstOrDefault();
                if(returnedUser == null)
                {
                    TempData["LoginError"] = "Email not registered. Register above";
                    return View("Index", lrmodel);
                }
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(returnedUser, returnedUser.password, submittedUser.loginUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["LoginError"] = "Incorrect password";
                    return View("Index", lrmodel);
                }
            }
            return View("Index", lrmodel);
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if((int?)HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index");
            }

            User loggedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
            List<Wedding> allWeddings = _context.weddings.ToList();
            List<RSVP> rsvps = _context.rsvps.Include(p => p.user).ToList();

            DashboardViewModel viewModel = new DashboardViewModel(){
                loggedUser = loggedUser,
                allWeddings = allWeddings,
                rsvps = rsvps
            };

            return View(viewModel);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Console.WriteLine("SESSION CLEARED");
            return RedirectToAction("Index");
        }
    }
}