using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BankAccounts.Models
{
    public class Account
    {
        public int id {get; set;}
        public int balance {get; set;}

        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}

        public int userid {get; set;}
        public User user {get;set;}

        public Account(int UserId)
        {
            userid = UserId;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
            balance = 0;
        }

    }
}