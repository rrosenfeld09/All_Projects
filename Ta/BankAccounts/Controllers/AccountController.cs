using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using System;

namespace BankAccounts.Controllers
{
    public class AccountController : Controller
    {
        public int id {get; set;}
        public float balance {get; set;}
        public string transaction {get; set;}
        public User user {get; set;}
        public int userid {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}

    }
}