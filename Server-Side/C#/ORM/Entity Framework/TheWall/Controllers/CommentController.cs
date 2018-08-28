using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;
using System.Linq;
using System.Collections.Generic;

namespace TheWall.Controllers
{
    public class CommentController : Controller
    {
        public MyContext _context {get; set;}

        public CommentController(MyContext context)
        {
            _context = context;
        }


        [HttpPost("create_comment")]
        public IActionResult CreateComment(Comment submittedComment)
        {
            if(ModelState.IsValid)
            {
                //pull logged user
                User returnedUser = _context.users.Where(p => p.user_id == (HttpContext.Session.GetInt32("loggedUser"))).FirstOrDefault();

                //create new comment
                Comment newComment = new Comment(returnedUser.user_id, submittedComment.message_id, submittedComment.comment);

                //add comment to DB
                _context.Add(newComment);
                _context.SaveChanges();

                return RedirectToAction("HomePage", "User");
            }

            return RedirectToAction("HomePage", "User");
        }

        [HttpGet("delete_comment/{comment_id}/{user_id}")]
        public IActionResult DeleteComment(int comment_id, int user_id)
        {
            //pull logged user
            int? loggedUser = HttpContext.Session.GetInt32("loggedUser");

            //pull comment to be deleted
            Comment commentToDelete = _context.comments.Where(p => p.comment_id == comment_id).FirstOrDefault();
            
            if(loggedUser == user_id)
            {
                _context.Remove(commentToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("HomePage", "User");
        }
    }
}