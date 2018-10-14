using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Email.Models
{
    public class Reply
    {
        [Key]
        public int reply_id {get; set;}

        public string message {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User user {get; set;}

        public int user_id {get;set;}

        public EmailMessage email {get; set;}

        public int email_id {get; set;}

        public Reply()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

    }

}