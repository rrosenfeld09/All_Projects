using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using System;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class Home : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("quotes")]
        public IActionResult NewQuote(Quote submittedQuote)
        {
            if(ModelState.IsValid)
            {
                DbConnector.Query($"INSERT INTO quotes(name, quote, created_on, updated_on) VALUES ('{submittedQuote.Name}', '{submittedQuote.QuoteString}', NOW(), NOW())");
                return RedirectToAction("AllQuotes");              
            }

            else
            {
                return View("Index");
            }
        }

        [HttpGet("quotes")]
        public IActionResult AllQuotes()
        {
            var AllQuotes = DbConnector.Query("SELECT * FROM quotes ORDER BY created_on DESC");
            ViewBag.AllQuotes = AllQuotes;
            return View();
        }

    }
}