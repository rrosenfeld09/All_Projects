using WeddingPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int wedding_id {get; set;}

        [Required]
        public string wedder_one {get; set;}

        [Required]
        public string wedder_two {get; set;}

        [Required]
        public DateTime wedding_date {get; set;}

        [Required]
        public string wedding_address {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User user {get; set;}

        public int user_id {get; set;}

        public List<RSVP> rsvps {get; set;}

        public Wedding(int _user_id)
        {
            rsvps = new List<RSVP>();
            user_id = _user_id;
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }

        public Wedding()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}