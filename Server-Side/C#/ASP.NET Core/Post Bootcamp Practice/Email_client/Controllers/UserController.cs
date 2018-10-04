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

        [HttpGet("inbox")]
        public IActionResult Inbox()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index", "Main");
            }

            User loggedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();

            InboxViewModel viewModel = new InboxViewModel();

            viewModel.loggedUser = loggedUser;

            viewModel.emails = _context.emails
                .Where(p => p.send_to_email == loggedUser.email)
                .Include(p => p.user)
                .ToList();

            return View(viewModel);
        }
    }
}