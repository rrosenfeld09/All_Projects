using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;
using System.Linq;
using System.Collections.Generic;

namespace TheWall.Controllers
{
    public class MessageController : Controller
    {
        public MyContext _context;

        public MessageController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("add_message")]
        public IActionResult CreateMessage(Message submittedMessage)
        {
            if(ModelState.IsValid)
            {
                int user_id = (int)HttpContext.Session.GetInt32("loggedUser");
                Message newMessage = new Message(user_id, submittedMessage.message);


                //save new message to db
                _context.Add(newMessage);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "User");
            }
            return RedirectToAction("HomePage", "User");

        }
    }
}