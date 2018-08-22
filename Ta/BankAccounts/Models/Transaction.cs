using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using BankAccounts.Controllers;

namespace BankAccounts.Models
{
    public class Transaction
    {
        public int id {get; set;}
        public int amount {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}
        public User user {get; set;}
        public int userid {get; set;}

        public Transaction(User submittedUser)
        {
            user = submittedUser;
            userid = submittedUser.userid;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }

        public Transaction()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}
