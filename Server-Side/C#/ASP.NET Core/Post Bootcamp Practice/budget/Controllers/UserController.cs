using System;
using System.Collections.Generic;
using System.Linq;
using budget.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Identity;

namespace budget.Controllers
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

        [HttpGet("sendtext")]
        public IActionResult SendText(string content)
        {
 

            return RedirectToAction("Index");
        }

        [HttpPost("newuser/post")]
        public IActionResult CreateUser(User submittedUser)
        {
            if(ModelState.IsValid)
            {
                if(submittedUser.password == submittedUser.confirm_pw)
                {
                    List<User> listOfUsers = _context.users
                                                    .Where(p => p.email == submittedUser.email)
                                                    .ToList();
                    if(listOfUsers.Count == 0)
                    {

                        PasswordHasher<User> Hasher = new PasswordHasher<User>();
                        submittedUser.password = Hasher.HashPassword(submittedUser, submittedUser.password);
                        _context.users.Add(submittedUser);
                        _context.SaveChanges();

                        const string accountSid = "ACc4fa038eedca363d0651905c37bcf9ac";
                        const string authToken = "484751994e76ea4d899fa5efc8b20614";

                        TwilioClient.Init(accountSid, authToken);

                        var message = MessageResource.Create(
                            body: $"NEW USER REGISTERED: Name: {submittedUser.name}, Email: {submittedUser.email}, Time Registered: {DateTime.Now}",
                            from: new Twilio.Types.PhoneNumber("+15403025243"),
                            to: new Twilio.Types.PhoneNumber("+15405882045")
                        );
                        return RedirectToAction("Index");

                    }
                    TempData["error"] = "Already registered, please sign in";
                    return View("Index");
                }
                TempData["error"] = "Passwords don't match";
            }
            return View("Index");
        }
    }
}