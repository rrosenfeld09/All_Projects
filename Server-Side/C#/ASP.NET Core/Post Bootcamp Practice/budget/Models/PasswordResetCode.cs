using System;
using System.ComponentModel.DataAnnotations;


namespace budget.Models
{
    public class PasswordResetCode
    {
        [Key]
        public int code_id {get; set;}

        public string code {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public PasswordResetCode()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}