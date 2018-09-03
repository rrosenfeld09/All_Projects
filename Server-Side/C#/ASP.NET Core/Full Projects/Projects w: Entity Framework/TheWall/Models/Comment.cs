using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int comment_id {get; set;}

        [Required]
        public string comment {get; set;}
        
        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Message message {get; set;}

        public int message_id {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        public Comment(int _user_id, int _message_id, string _comment)
        {
            user_id = _user_id;
            message_id = _message_id;
            comment = _comment;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }

        public Comment()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}