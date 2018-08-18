using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace BankAccounts.Models
{
    public class User
    {
        [Key]
        public int id {get; set;}
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

        public Account account {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}

        public User()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}