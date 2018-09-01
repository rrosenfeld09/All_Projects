//improvements:
//1: make is user logged in method

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string first_name {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string last_name {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        [EmailAddress]
        public string email {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string password {get; set;}

        [NotMapped]
        [Required(ErrorMessage = "Can't be blank")]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Wedding> weddings {get; set;}

        public List<RSVP> rsvps {get; set;}

        public User()
        {
            rsvps = new List<RSVP>();
            weddings = new List<Wedding>();
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }

    public class Login
    {
        [Required(ErrorMessage = "Can't be blank")]
        public string email {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string password {get; set;}
    }
}