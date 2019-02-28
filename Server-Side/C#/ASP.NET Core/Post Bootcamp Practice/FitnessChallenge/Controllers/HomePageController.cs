using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FitnessChallenge.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FitnessChallenge.Controllers
{
    public class HomePageController : BaseEntity
    {
        public MyContext _context;

        public HomePageController(MyContext context)
        {
            _context = context;
        }
        
        [HttpGet("homepage")]
        public IActionResult HomePage()
        {
            if(IsUserInSession())
            {
                User returnedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                HomePageVM viewModel = new HomePageVM();
                viewModel.user = returnedUser;
                viewModel.comments = _context.comments.OrderByDescending(p => p.created_at).Take(10).ToList();
                viewModel.userRanking = _context.users.OrderByDescending(p => p.total_points).Take(10).ToList();
                return View(viewModel);
            }

            return RedirectToAction("Index", "User");
        }


        [HttpPost("add/comment")]
        public IActionResult AddComment(int user_id, string user_nickname, string comment, int avatar_id)
        {
            if(IsUserInSession())
            {
                Comment commentToAdd = new Comment();
                commentToAdd.user_id = user_id;
                commentToAdd.user_nickname = user_nickname;
                commentToAdd.avatar_id = avatar_id;
                commentToAdd.comment = comment;
                _context.comments.Add(commentToAdd);
                _context.SaveChanges();
                return RedirectToAction("HomePage");
            }

            return RedirectToAction("Index", "User");
        }

        [HttpGet("delete/{comment_id}/{user_id}")]
        public IActionResult DeleteComment(int comment_id, int user_id)
        {
            if(IsUserInSession())
            {
                if(user_id == HttpContext.Session.GetInt32("loggedUser"))
                {
                    Comment commentToDelete = _context.comments.Where(p => p.comment_id == comment_id).FirstOrDefault();
                    _context.Remove(commentToDelete);
                    _context.SaveChanges();
                }
                return RedirectToAction("HomePage");
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("history")]
        public IActionResult MyHistory()
        {
            if(IsUserInSession())
            {
                HistoryViewModel viewModel = new HistoryViewModel();
                viewModel.logs = _context.logs.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).OrderByDescending(p => p.created_at).ToList();
                return View(viewModel);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("darulz")]
        public IActionResult DaRulz()
        {
            if(IsUserInSession())
            {
                return View();
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("do_something")]
        public IActionResult DoSomething()
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("haha, nice...");
            Console.WriteLine("==============================================================================");
            return RedirectToAction("HomePage");
        }
    }
}