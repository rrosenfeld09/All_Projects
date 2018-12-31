using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FitnessChallenge.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FitnessChallenge.Controllers
{
    public class UserController : BaseEntity
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

        [HttpPost("login_action")]
        public IActionResult Login(LoginUser submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                if(returnedUser == null)
                {
                    ViewData["error"] = "email not registered, please sign up";
                    return View("index");
                }

                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(returnedUser, returnedUser.password, submittedUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", (int)returnedUser.user_id);
                    return RedirectToAction("HomePage", "HomePage");
                }
                ViewData["error"] = "username/password incorrect";
                return View("index");
            }
            return View("index");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register/post")]
        public IActionResult RegisterAction(User submittedUser)
        {
            if(IsUserInSession() == false)
            {
                if(ModelState.IsValid)
                {
                    User returnedUser = _context.users.Where(p => p.email == submittedUser.email).FirstOrDefault();

                    if(returnedUser != null)
                    {
                        ViewData["error"] = "email already registered, please sign in.";
                        return View("register");
                    }
                    User nicknameCheck = _context.users.Where(p => p.nickname == submittedUser.nickname).FirstOrDefault();

                    if(nicknameCheck != null)
                    {
                        ViewData["error"] = "nickname already taken, please choose another";
                        return View("register");
                    }

                    if(submittedUser.password != submittedUser.confirm_pw)
                    {
                        ViewData["error"] = "passwords don't match";
                        return View("register");
                    }

                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    submittedUser.password = Hasher.HashPassword(submittedUser, submittedUser.password);
                              
                    _context.users.Add(submittedUser);
                    _context.SaveChanges();

                    User registeredUser = _context.users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                    HttpContext.Session.SetInt32("loggedUser", (int)registeredUser.user_id);

                    return RedirectToAction("SelectAvatar");
                }
            return View("register");
            }
            return RedirectToAction("HomePage", "HomePage");
        }

        [HttpGet("select_avatar")]
        public IActionResult SelectAvatar()
        {
            if(IsUserInSession())
            {
                User returnedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                if(returnedUser.avatar_id == 0)
                {
                    return View();
                }
                return RedirectToAction("HomePage", "HomePage");
            }
            return RedirectToAction("Index");
        }

        [HttpPost("select_avatar_put")]
        public IActionResult SelectAvatarPut(int avatar_img_id)
        {
            if(IsUserInSession())
            {
                User returnedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                returnedUser.avatar_id = avatar_img_id;
                _context.SaveChanges();
                return RedirectToAction("HomePage", "HomePage");
            }
            
            return RedirectToAction("index");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost("add_exercise_points")]
        public IActionResult AddExercisePoints(int user_id)
        {
            if(IsUserInSession())
            {
                if(user_id == HttpContext.Session.GetInt32("loggedUser"))
                {
                    User returnedUser = _context.users.Where(p => p.user_id == user_id).FirstOrDefault();
                    returnedUser.exercise_points += 1;
                    _context.SaveChanges();
                    return RedirectToAction("HomePage", "HomePage");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost("add_eating_points")]
        public IActionResult AddEatingPoints(int user_id)
        {
            if(IsUserInSession())
            {
                if(user_id == HttpContext.Session.GetInt32("loggedUser"))
                {
                    User returnedUser = _context.users.Where(p => p.user_id == user_id).FirstOrDefault();
                    returnedUser.eating_points += 1;
                    _context.SaveChanges();
                    return RedirectToAction("HomePage", "HomePage");
                }
            }
            return RedirectToAction("Index");
        }


    }
}

