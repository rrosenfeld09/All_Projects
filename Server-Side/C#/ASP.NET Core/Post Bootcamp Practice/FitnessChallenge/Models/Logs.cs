using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace FitnessChallenge.Models
{
    public class Log
    {
        [Key]
        public int log_id {get; set;}
        
        public float num_points {get; set;}

        public string description {get; set;}



        public int user_id {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Log()
        {
            created_at = DateTime.UtcNow.AddHours(-5);
            updated_at = DateTime.UtcNow.AddHours(-5);
            
        }
    }
}