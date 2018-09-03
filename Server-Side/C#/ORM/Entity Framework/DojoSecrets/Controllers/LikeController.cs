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
    public class LikeController : Controller
    {
        public MyContext _context;

        public LikeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("like/{secret_id}/{user_id}")]
        public IActionResult Like(int secret_id, int user_id)
        {
            if(user_id != HttpContext.Session.GetInt32("loggedUser"))
            {
                return RedirectToAction("HomePage", "User");
            }
            Like newLike = new Like()
            {
                user_id = user_id, 
                secret_id = secret_id
            };

            //create new like
            _context.Add(newLike);
            _context.SaveChanges();

            //update num_likes property on Secret object
            Secret returnedSecret = _context.secrets.Where(p => p.secret_id == secret_id).FirstOrDefault();
            returnedSecret.num_likes += 1;
            _context.SaveChanges();
            return RedirectToAction("HomePage", "User");
        }

        [HttpGet("unlike/{secret_id}/{user_id}")]
        public IActionResult Unlike(int secret_id, int user_id)
        {
            if(user_id != HttpContext.Session.GetInt32("loggedUser"))
            {
                return RedirectToAction("HomePage", "User");
            }

            //remove like object
            Like likeToDelete = _context.likes.Where(p => p.secret_id == secret_id).Where(p => p.user_id == user_id).FirstOrDefault();
            _context.Remove(likeToDelete);
            _context.SaveChanges();

            //update num_lies property on Secret object
            Secret returnedSecret = _context.secrets.Where(p => p.secret_id == secret_id).FirstOrDefault();
            returnedSecret.num_likes -= 1;
            _context.SaveChanges();

            return RedirectToAction("HomePage", "User");
        }
    }
}