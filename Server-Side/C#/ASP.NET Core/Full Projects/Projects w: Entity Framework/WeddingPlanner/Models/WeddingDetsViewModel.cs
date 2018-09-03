using WeddingPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class WeddingDetsViewModel
    {
        public Wedding wedding{get; set;}

        public List<RSVP> rsvps {get; set;}
    }
}