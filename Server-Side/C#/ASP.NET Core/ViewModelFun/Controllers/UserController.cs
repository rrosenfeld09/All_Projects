using Microsoft.AspNetCore.Mvc;
using ViewModelFun.Models;
using System.Collections.Generic;

namespace ViewModelFun.Controllers
{
    public class User : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            Message message = new Message()
            {
                MessageContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Excepturi aliquid odit, accusantium neque id nisi soluta ducimus culpa beatae laboriosam molestiae molestias magnam voluptatum accusamus blanditiis repudiandae atque illum consequuntur."
            };
            return View(message);
        }

        [HttpGet("numbers")]
        public IActionResult Numbers()
        {
            int[] args = new int[] {1,2,3,4,5,6,7,8,9};
            Numbers new_num = new Numbers(args);
            return View(new_num);
        }

        [HttpGet("users")]
        public IActionResult UserList()
        {

            Users richard = new Users("Richard", "R");
            Users virginia = new Users("Virginia", "B");
            Users brad = new Users("Brad", "D");
            Users john = new Users("John", "M");

            List<Users> args = new List<Users>() {richard, virginia, brad, john};

            UsersList final = new UsersList(args);
            
            return View(final);
        }

        [HttpGet("user")]
        public IActionResult Users()
        {
            Users richard = new Users("Richard", "R");
            return View(richard);
        }


    }

}