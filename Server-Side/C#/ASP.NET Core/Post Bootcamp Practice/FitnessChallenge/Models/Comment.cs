using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace FitnessChallenge.Models
{
    public class Comment
    {
        [Key]
        public int comment_id {get; set;}

        [Required]
        public string comment {get; set;}

        public int user_id {get; set;}

        public string user_nickname {get; set;}

        public int avatar_id {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Comment()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}