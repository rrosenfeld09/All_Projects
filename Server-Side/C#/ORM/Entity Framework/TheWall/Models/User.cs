using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;



namespace TheWall.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required]
        public string first_name {get; set;}

        [Required]
        public string last_name {get; set;}

        [Required]
        [EmailAddress]
        public string email {get; set;}

        [Required]
        public string password {get; set;}

        [Required]
        [NotMapped]
        public string confirm_password {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Message> messages {get; set;}

        public List<Comment> comments {get; set;}

        public User()
        {
            messages = new List<Message>();
            comments = new List<Comment>();
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }

    public class LoginUser
    {
        [Required]
        public string email {get; set;}

        [Required]
        public string password {get; set;}
    }
}