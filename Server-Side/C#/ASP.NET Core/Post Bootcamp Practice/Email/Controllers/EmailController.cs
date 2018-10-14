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

        [HttpGet("email_details/{email_id}")]
        public IActionResult EmailDetails(int email_id)
        {
            EmailMessage email = _context.emails
                .Where(p => p.email_id == email_id)
                .Include(p => p.user)
                .FirstOrDefault();

            ReplyViewModel viewModel = new ReplyViewModel();

            viewModel.replies = _context.replies.Where(p => p.email_id == email_id).ToList();

            viewModel.email = email;

            return View(viewModel);
        }

        [HttpPost("reply_action")]
        public IActionResult ReplyAction(ReplyViewModel submittedReply)
        {
            if(ModelState.IsValid)
            {
                submittedReply.reply.user_id = (int)HttpContext.Session.GetInt32("loggedUser");
                _context.replies.Add(submittedReply.reply);
                _context.SaveChanges();
                return RedirectToAction("EmailDetails", new{email_id = submittedReply.reply.email_id});
            }

            return RedirectToAction("EmailDetails", new{email_id = submittedReply.reply.email_id});
        }
    }
}