using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using WeddingPlanner.Models;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class DashboardViewModel
    {
        public User loggedUser {get; set;}

        public List<Wedding> allWeddings {get; set;}

        public List<RSVP> rsvps {get; set;}
    }
}