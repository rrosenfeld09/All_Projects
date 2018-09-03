using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Models;
using Microsoft.AspNetCore.Http;
using LostInTheWoods.Factories;
using System.Collections.Generic;
using System;

namespace LostInTheWoods.Controllers
{
    public class TrailController : Controller
    {
        private readonly TrailFactory trailFactory;
        public TrailController()
        {
            trailFactory = new TrailFactory();
        }



        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllTrails = trailFactory.GetAllTrails();   
            return View();
        }

        [HttpGet("/NewTrail")]        public IActionResult NewTrail()
        {
            return View();
        }

        [HttpPost("process_add")]
        public IActionResult ProcessAdd(Trail submittedTrail)
        {
            if (ModelState.IsValid)
            {
                trailFactory.AddNewTrail(submittedTrail);
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewTrail");
            }
        }

        [HttpGet("trails/{Id}")]
        public IActionResult TrailDescription(int Id)
        {
            Trail trail = trailFactory.GetSpecificTrail(Id);
            return View(trail);
        }
    }
} 