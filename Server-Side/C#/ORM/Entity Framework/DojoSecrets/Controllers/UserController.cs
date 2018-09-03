using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DojoSecrets.Models;

namespace DojoSecrets.Controllers
{
    public class UserController : Controller 
    {

        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        public bool IsUserLoggedIn()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(IsUserLoggedIn())
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }

        [HttpPost("create_user")]
        public IActionResult CreateUser(LogRegViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                if(submittedUser.user.password != submittedUser.user.confirm_pw)
                {
                    TempData["PasswordError"] = "Passwords don't match";
                    return View("Index");
                }
                if(_context.users.Any(p => p.email == submittedUser.user.email))
                {
                    TempData["AlreadyRegisteredError"] = "Email already registered, please login.";
                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                submittedUser.user.password = Hasher.HashPassword(submittedUser.user, submittedUser.user.password);
                
                _context.Add(submittedUser.user);
                _context.SaveChanges();

                User returnedUser = _context.users.Where(p => p.email == submittedUser.user.email).FirstOrDefault();

                HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost("login_action")]
        public IActionResult LoginAction(LogRegViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.email == submittedUser.loginUser.email).FirstOrDefault();
                if(returnedUser == null)
                {
                    TempData["LogginError"] = "Email not yet registered";
                    return View("Index");
                }

                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(returnedUser, returnedUser.password, submittedUser.loginUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                    return RedirectToAction("HomePage");
                }
                else
                {
                    TempData["LogginError"] = "Email/Password incorrect";
                    return View("Index");
                }
            }
            return View("Index");
        }

        [HttpGet("secrets")]
        public IActionResult HomePage()
        {
            if(IsUserLoggedIn() == false)
            {
                return RedirectToAction("Index");
            }
            HomePageViewModel viewModel = new HomePageViewModel()
            {
                user = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault(),
                secrets = _context.secrets.OrderByDescending(p => p.created_at).Take(5).ToList(),
                likes = _context.likes.ToList()
            };
            return View(viewModel);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}