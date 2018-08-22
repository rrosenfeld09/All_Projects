using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
 

namespace BankAccounts.Controllers
{
    public class AccountController : Controller
    {
        public MyContext _context;

        public AccountController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("accounts/{userid}")]
        public IActionResult HomePage(int userid)
        {
            User returnedUser = _context.Users.Where(p => p.userid == userid).FirstOrDefault();

            int? loggedUser = HttpContext.Session.GetInt32("userid");

            List<Transaction> everyTrans = _context.Transactions.Where(p => p.userid == loggedUser).ToList();
            var ViewThing = new UserTransactionViewModel(){user = returnedUser, tranList = everyTrans};
            Console.WriteLine("GOT TRANSACTIONS");

            return View(ViewThing);
        }
        [HttpGet("adjustmoney/{userid}")]
        public IActionResult AddWithdrawMoney(int userid, int dollar_amount)
        {
            User returnedUser = _context.Users.Where(p => p.userid == userid).FirstOrDefault();
            returnedUser.balance += dollar_amount;
            _context.SaveChanges();

            int? loggedUser = HttpContext.Session.GetInt32("userid");
            User userForTransaction = _context.Users.Where(p => p.userid == loggedUser).FirstOrDefault();

            Transaction newTransaction = new Transaction(userForTransaction);
            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();

            return RedirectToAction("HomePage");
        }
    }
}