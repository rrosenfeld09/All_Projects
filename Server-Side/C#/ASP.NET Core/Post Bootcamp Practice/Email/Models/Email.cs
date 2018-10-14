using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Email.Models
{
    public class EmailMessage
    {
        [Key]
        public int email_id {get; set;}

        [Required]
        public string send_to_email {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        [Required]
        public string message {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Reply> replies {get; set;}

        public EmailMessage()
        {
            replies = new List<Reply>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}