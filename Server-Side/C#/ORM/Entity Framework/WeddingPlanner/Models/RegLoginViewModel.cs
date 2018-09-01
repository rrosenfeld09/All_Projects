using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using WeddingPlanner.Models;

namespace WeddingPlanner.Models
{
    public class RegLoginViewModel
    {
        public User regUser {get; set;}

        public Login loginUser {get; set;}
    }
}