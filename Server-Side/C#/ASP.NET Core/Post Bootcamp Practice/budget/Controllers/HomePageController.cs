using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace budget.Controllers
{
    public class HomePageController : BaseEntity
    {
        [HttpGet("homepage/get")]
        public IActionResult HomePage()
        {
            if(IsUserInSession())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}