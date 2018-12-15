using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitnessChallenge.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required (ErrorMessage= "your username can't be blank")]
        public string username {get; set;}

        [Required (ErrorMessage = "your password can't be blank")]
        public string password {get; set;}
    }
}