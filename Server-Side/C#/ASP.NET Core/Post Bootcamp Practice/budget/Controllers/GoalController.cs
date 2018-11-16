using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using budget.Models;
using System.Linq;


namespace budget.Controllers
{
    public class GoalController : BaseEntity
    {
        public MyContext _context;

        public GoalController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("goals")]
        public IActionResult CreateProfileProperties()
        {
            if(IsUserInSession())
            {
                Goal returnedGoal = _context.goals.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                if(returnedGoal != null)
                {
                    return RedirectToAction("HomePage", "User");
                }
                return View();
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost("goals/post")]
        public IActionResult CreateGoal(Goal submittedGoal)
        {
            if(IsUserInSession())
            {
                if(ModelState.IsValid)
                {
                    submittedGoal.user_id = (int)HttpContext.Session.GetInt32("loggedUser");
                    _context.goals.Add(submittedGoal);
                    _context.SaveChanges();
                    
                    return RedirectToAction("HomePage", "User");
                }
                return View("createprofileproperties");
            }
            return RedirectToAction("Index", "User");

        }
    }
}