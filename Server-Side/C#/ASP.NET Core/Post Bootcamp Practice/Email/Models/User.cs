using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Email.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required]
        public string email {get; set;}

        [Required]
        public string password {get; set;}

        [NotMapped]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<EmailMessage> emails {get; set;}

        public List<Reply> replies {get; set;}

        public User()
        {
            replies = new List<Reply>();
            emails = new List<EmailMessage>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
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