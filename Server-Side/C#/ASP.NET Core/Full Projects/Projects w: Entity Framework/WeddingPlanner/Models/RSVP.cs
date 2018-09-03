using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int rsvp_id {get; set;}

        public int wedding_id {get; set;}

        public int user_id {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Wedding wedding {get; set;}

        public User user {get; set;}

        public RSVP()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}