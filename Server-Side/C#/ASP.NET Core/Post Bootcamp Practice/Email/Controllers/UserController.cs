using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Email.Models;


namespace Email.Controllers
{
    public class UserController : Controller
    {
        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }


        [HttpPost("register_action")]
        public IActionResult RegisterAction(LogReg submittedUser)
        {
            if(ModelState.IsValid)
            {
                _context.users.Add(submittedUser.user);
                _context.SaveChanges();

                User returnedUser = _context.users.Where(p => p.email == submittedUser.user.email).FirstOrDefault();
                HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                return RedirectToAction("Inbox");
            }

            return View("Index");
        }

        [HttpPost("login_action")]
        public IActionResult LoginAction(LogReg submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.email == submittedUser.loginUser.email).FirstOrDefault();
                if(submittedUser.loginUser.password == returnedUser.password)
                {
                    HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                    return RedirectToAction("Inbox");
                }
            }
            return View("Index");
        }

        // adding joke feature
        string one = "Did you hear about the restaurant on the moon? Great food, no atmosphere";

        string two = "What do you call a fake noodle? An impasta";

        string three = "How many apples grow on a tree? All of them";
        
        string four = "I just watched a program about beavers. It was the best dam program I've ever seen";

        string five = "How does a penguin buikld it's house? Igloose it together";

        [HttpGet("/dadjoke")]
        public string Joke()
        {
            Random rand = new Random();
            int val = rand.Next(1, 5);

            string joke;

            if(val == 1)
            {
                joke = one;
            }
            else if(val == 2)
            {
                joke = two;
            }
            else if(val == 3)
            {
                joke = three;
            }
            else if(val == 4)
            {
                joke = four;
            }
            else
            {
                joke = five;
            }

            return (joke);
        }

        [HttpGet("inbox")]
        public IActionResult Inbox()
        {
            try
            {
                if(HttpContext.Session.GetInt32("loggedUser") == null)
                {
                    return RedirectToAction("Index", "Main");
                }

                User loggedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();

                InboxViewModel viewModel = new InboxViewModel();

                viewModel.loggedUser = loggedUser;
                
                viewModel.joke = Joke();

                viewModel.emails = _context.emails
                    .Where(p => p.send_to_email == loggedUser.email)
                    .Include(p => p.user)
                    .ToList();

                return View(viewModel);
            }

            catch
            {
                return Json("Error");
            }
        }
    }
}