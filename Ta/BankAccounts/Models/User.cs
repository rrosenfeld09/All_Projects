using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BankAccounts.Models
{
    public class User
    {
        [Key]
        public int userid {get; set;}
        [Required]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        public string first_name {get; set;}
        [Required]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        public string last_name {get; set;}
        [Required]
        [EmailAddress]
        public string email {get; set;}
        [Required]
        [MinLength(8, ErrorMessage = "must be at least 8 characters long")]
        public string password {get; set;}

        public int balance {get; set;}

        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}

        public List<Transaction> transactions {get; set;}

        public User()
        {
            transactions = new List<Transaction>();
            balance = 0;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}