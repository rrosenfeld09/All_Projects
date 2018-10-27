using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace budget.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required (ErrorMessage = "Name can't be blank")]
        public string name {get; set;}

        [Required (ErrorMessage = "Email address can't be blank")]

        public string email {get; set;}

        [Required (ErrorMessage = "Password can't be blank")]

        public string password {get; set;}

        [Required (ErrorMessage = "Password confirmation can't be blank")]
        [NotMapped]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }

    public class LoginUser
    {
        [Required (ErrorMessage = "Email can't be blank")]
        public string email {get; set;}

        [Required (ErrorMessage = "Password can't be blank")]
        public string password {get; set;}
    }
}