using System;
using System.Collections.Generic;
using System.Linq;
using budget.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace budget.Controllers
{
    public class UserController : BaseEntity
    {
        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        const string accountSid = "ACc4fa038eedca363d0651905c37bcf9ac";
        const string authToken = "484751994e76ea4d899fa5efc8b20614";


        [HttpGet("")]
        public IActionResult Index()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("HomePage", "HomePage");
            }
            else 
            {
                return View();
            }
        }


        [HttpPost("newuser/post")]
        public IActionResult CreateUser(IndexViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                if(submittedUser.user.password == submittedUser.user.confirm_pw)
                {
                    List<User> listOfUsers = _context.users
                                                    .Where(p => p.email == submittedUser.user.email)
                                                    .ToList();
                    if(listOfUsers.Count == 0)
                    {
                        //hash password and save to DB
                        PasswordHasher<User> Hasher = new PasswordHasher<User>();
                        submittedUser.user.password = Hasher.HashPassword(submittedUser.user, submittedUser.user.password);
                        _context.users.Add(submittedUser.user);
                        _context.SaveChanges();

                        //call Twilio API to send text string
                        TwilioClient.Init(accountSid, authToken);

                        var message = MessageResource.Create(
                            body: $"NEW USER REGISTERED: Name: {submittedUser.user.name}, Email: {submittedUser.user.email}, Time Registered: {DateTime.Now}",
                            from: new Twilio.Types.PhoneNumber("+15403025243"),
                            to: new Twilio.Types.PhoneNumber("+15405882045")
                        );

                        //save user in session
                        User returnedUser = _context.users.Where(p => p.email == submittedUser.user.email)
                                                            .FirstOrDefault();

                        HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);

                        return RedirectToAction("HomePage", "HomePage");

                    }
                    TempData["error"] = "Already registered, please sign in";
                    return View("Index");
                }
                TempData["error"] = "Passwords don't match";
            }
            return View("Index");
        }

        [HttpPost("login/post")]
        public IActionResult Login(IndexViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                User userToCheck = _context.users.Where(p => p.email == submittedUser.loginUser.email).FirstOrDefault();

                if(userToCheck == null)
                {
                    TempData["loginerror"] = "Email not registered yet. Please register above.";
                    return View("Index");
                }

                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(userToCheck, userToCheck.password, submittedUser.loginUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", userToCheck.user_id);
                    Console.WriteLine($"Successfully logged in user_id {userToCheck.user_id}");
                    return RedirectToAction("HomePage", "HomePage");
                }
                else 
                {
                    TempData["loginerror"] = "Incorrect password";
                }
            }
            
            return View("Index");
        }

    }
}