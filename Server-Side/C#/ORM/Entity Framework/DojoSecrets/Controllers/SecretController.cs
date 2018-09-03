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
    public class SecretController : Controller
    {
        public MyContext _context; 

        public SecretController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("create_secret")]
        public IActionResult CreateSecret(HomePageViewModel submittedSecret)
        {
            if(ModelState.IsValid)
            {
                submittedSecret.secret.user_id = (int)HttpContext.Session.GetInt32("loggedUser");
                _context.Add(submittedSecret.secret);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "User");
            }
            return RedirectToAction("HomePage", "User");
        }

        [HttpGet("delete/{secret_id}/{user_id}")]
        public IActionResult Delete(int secret_id, int user_id)
        {
            Secret secretToDelete = _context.secrets.Where(p => p.secret_id == secret_id).FirstOrDefault();
            if(secretToDelete.user_id == user_id)
            {
                _context.Remove(secretToDelete);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "User");
            }
            return RedirectToAction("HomePage", "User");
        }

        [HttpGet("mostpopular")]
        public IActionResult MostPopular()
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index");
            }
            MostPopularViewModel viewModel = new MostPopularViewModel()
            {
                user = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault(),
                secrets = _context.secrets.OrderByDescending(p => p.num_likes).ToList(),
                likes = _context.likes.ToList()
            };
            return View(viewModel);
        }
    }
}