using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;


namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int id {get; set;}

        [Required(ErrorMessage = "This field is required!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letter, please")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters long")]
        public string first_name {get; set;}

        [Required(ErrorMessage = "This field is required!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letter, please")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters long")]
        public string last_name {get; set;}

        [Required(ErrorMessage = "This field is required!")]
        [EmailAddress(ErrorMessage = "That's not a valid email address!")]
        public string email {get; set;}

        [Required(ErrorMessage = "This field is required!")]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
        public string password {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }

    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string email {get; set;}
        [Required]
        public string password {get; set;}
    }
}