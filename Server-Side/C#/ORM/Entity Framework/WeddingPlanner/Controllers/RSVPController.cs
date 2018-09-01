using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Controllers
{
    public class RSVPController : Controller
    {
        public MyContext _context;

        public RSVPController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("rsvp/{wedding_id}")]
        public IActionResult RSVPAction(int wedding_id)
        {
            if((int?)HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return RedirectToAction("Index", "User");
            }
            int _user_id = (int)HttpContext.Session.GetInt32("loggedUser");
            RSVP newRSVP = new RSVP()
            {
                user_id = _user_id,
                wedding_id = wedding_id
            };
            _context.Add(newRSVP);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }

        [HttpGet("unrsvp/{user_id}/{wedding_id}")]
        public IActionResult unRSVP(int user_id, int wedding_id)
        {
            RSVP returnedRSVP = _context.rsvps.Where(p => p.user_id == user_id).Where(p => p.wedding_id == wedding_id).FirstOrDefault();
            _context.Remove(returnedRSVP);
            _context.SaveChanges();

            return RedirectToAction("Dashboard", "User");
        }
    }
}