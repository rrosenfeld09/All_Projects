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
    public class EmailController : Controller
    {
        public MyContext _context;

        public EmailController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("write_email")]
        public IActionResult WriteEmail()
        {
            return View();
        }

        [HttpPost("email_action")]
        public IActionResult EmailAction(EmailMessage submittedEmail)
        {
            if(ModelState.IsValid)
            {
                EmailMessage emailToSend = new EmailMessage();

                submittedEmail.user_id = (int)HttpContext.Session.GetInt32("loggedUser");

                _context.emails.Add(submittedEmail);
                _context.SaveChanges();

                return RedirectToAction("Inbox", "User");
            }
            return View("WriteEmail");
        }
    }
}