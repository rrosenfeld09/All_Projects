
using System;
using Microsoft.AspNetCore.Mvc;

using ViewModel.Models;

namespace ViewModel
{
    public class Main : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            Message new_message = new Message()
            {
                MessageContent = "orem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };            
            return View(new_message);
        }

        [HttpGet]
        [Route("numbers")]
        public IActionResult Numbers()
        {
            Numbers new_list = new Numbers()
            {
                nums = {1,2,3,4,5}
            };
            return View("Numbers", new_list);
        }

        [HttpGet]
        [Route("users")]
        public IActionResult Users()
        {
            Users user_list = new Users()
            {
                users = {"Richard", "Viringia", "John", "Brad"}
            };
            return View("Users", user_list);
        }

        [HttpGet]
        [Route("user")]
        public IActionResult User()
        {
            User new_user = new User()
            {
                FirstName = "Richard", 
                LastName = "Rosenfeld"
            };
            return View("User", new_user);
        }
    }
}