using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTauranter.Models;
using System.Linq;
using System.Collections.Generic;

namespace RESTauranter.Controllers
{
    public class ReviewController : Controller
    {
        private RestaurantContext _context;

        public ReviewController(RestaurantContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("add_review")]
        public IActionResult AddReview(Review submittedReview)
        {
            if(ModelState.IsValid)
            {
                _context.Add(submittedReview);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }

        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            IEnumerable<Review> allReviews = _context.Reviews.OrderByDescending(p => p.created_at).ToList();
            return View(allReviews);
        }
    }
}