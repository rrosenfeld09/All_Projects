using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;



namespace FitnessChallenge.Models
{
    
    public class User
    {
        [Key]
        public long user_id {get; set;}

        [Required]
        [EmailAddress (ErrorMessage = "email can't be blank")]
        public string email {get; set;}

        
        [Required]
        [MinLength(4, ErrorMessage="nickname must be at least 4 characters")]
        [MaxLength(20, ErrorMessage="nickname can't be longer than 20 characters")]
        public string nickname {get; set;}

        public int avatar_id {get; set;}

        [Required]
        [MinLength(5, ErrorMessage="password must be longer than 5 characters")]
        [MaxLength(20, ErrorMessage="password can't be longer than 20 characters")]
        public string password {get; set;}

        [Required(ErrorMessage="password confirmation can't be blank")]
        [NotMapped]
        public string confirm_pw {get; set;}

        public float exercise_points {get;set;}

        public float eating_points {get; set;}

        public float total_points {get; set;}


        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User()
        {
            exercise_points = 0;
            eating_points = 0;
            total_points = 0;
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
    
    public class LoginUser
    {

        [Required (ErrorMessage= "your email can't be blank")]
        [EmailAddress]
        public string email {get; set;}

        [Required (ErrorMessage = "your password can't be blank")]
        public string password {get; set;}

    }
}