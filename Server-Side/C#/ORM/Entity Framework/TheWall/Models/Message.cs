using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class Message 
    {
        [Key]
        public int message_id {get; set;}
        
        [Required]
        public string message {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        List<Comment> comments {get; set;}

        public Message(int _user_id, string _message)
        {
            comments = new List<Comment>();
            message = _message;
            user_id = _user_id;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }

        public Message()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow; 
        }
    }
}