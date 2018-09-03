using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        public MyContext _context;

        public WeddingController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("new_wedding")]
        public IActionResult NewWedding()
        {
            return View();
        }

        [HttpPost("create_wedding")]
        public IActionResult CreateWedding(Wedding submittedWedding)
        {
            if((int?)HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index", "User");
            }
            if(ModelState.IsValid)
            {
                submittedWedding.user_id = (int)HttpContext.Session.GetInt32("loggedUser");
                _context.Add(submittedWedding);
                _context.SaveChanges();
                return RedirectToAction("WeddingDetails", new {wedding_id = submittedWedding.wedding_id});
            }
            return View("newwedding");
        }

        [HttpGet("wedding_details/{wedding_id}")]
        public IActionResult WeddingDetails(int wedding_id)
        {
            if((int?)HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index", "User");
            }
            Wedding wedding = _context.weddings.Where(p => p.wedding_id == wedding_id).FirstOrDefault();
            List<RSVP> rsvps = _context.rsvps.Where(p => p.wedding_id == wedding.wedding_id).Include(p => p.user).ToList();

            WeddingDetsViewModel viewModel = new WeddingDetsViewModel(){wedding = wedding, rsvps = rsvps};
            return View(viewModel);
        }

        [HttpGet("/delete/{wedding_id}")]
        public IActionResult deleteWedding(int wedding_id)
        {
            Wedding returnedWedding = _context.weddings.Where(p => p.wedding_id == wedding_id).FirstOrDefault();

            _context.Remove(returnedWedding);
            _context.SaveChanges();

            return RedirectToAction("Dashboard", "User");
        }
    }
}