using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;


namespace DojoSecrets.Models
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
        [MinLength(8, ErrorMessage = "Password Must be at least 8 characters long")]
        public string password {get; set;}

        [NotMapped]
        [Required(ErrorMessage = "Can't be blank")]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Secret> secrets {get; set;}

        public List<Like> likes {get; set;}

        public User()
        {
            secrets = new List<Secret>();
            likes = new List<Like>();
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }

    public class LoginUser
    {
        [Required(ErrorMessage = "Can't be blank")]
        [EmailAddress]
        public string email {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string password {get; set;}
    }
}