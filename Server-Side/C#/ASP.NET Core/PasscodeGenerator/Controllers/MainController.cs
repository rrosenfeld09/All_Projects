using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace PasscodeGenerator.Controllers
{
    public class MainController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? passcodeNum = HttpContext.Session.GetInt32("num");

            if (passcodeNum == null)
            {
                HttpContext.Session.SetInt32("num", 1);
                passcodeNum = HttpContext.Session.GetInt32("num");

            }
            else
            {
                HttpContext.Session.SetInt32("num", (int)HttpContext.Session.GetInt32("num") + 1);
                passcodeNum = HttpContext.Session.GetInt32("num");
            }

            string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            char[] newArr = new char[14];
            Random rand = new Random();

            for(int i = 0; i < 14; i++)
            {
                newArr[i] = Characters[rand.Next(Characters.Length)];
            }

            string finalPasscode = new string(newArr);

            ViewBag.Number = passcodeNum;
            ViewBag.Passcode = finalPasscode;
            return View();
        }
        [HttpGet("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}