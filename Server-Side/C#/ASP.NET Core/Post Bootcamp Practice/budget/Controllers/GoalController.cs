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

        [HttpPost("left_over_adj")]
        public IActionResult LeftOverAdj(string option, int user_id, int amount)
        {
            Goal returnedGoal = _context.goals.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();

            if(user_id == HttpContext.Session.GetInt32("loggedUser") && amount > 0)
            {
                if(option == "debt_payoff")
                {
                    returnedGoal.total_debt -= amount;
                }
                if(option == "savings")
                {
                    returnedGoal.total_savings += amount;
                }
                _context.SaveChanges();
            }

             return RedirectToAction("HomePage", "User");
        }
    }
}