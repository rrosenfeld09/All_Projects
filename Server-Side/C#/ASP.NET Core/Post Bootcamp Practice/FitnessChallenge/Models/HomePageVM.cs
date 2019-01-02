using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using FitnessChallenge.Models;
using System.Collections.Generic;

namespace FitnessChallenge.Models
{
    public class HomePageVM
    {
        public User user {get; set;}

        public List<Comment> comments {get; set;}

        public List<User> userRanking {get; set;}
    }
    
}