using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using TheWall.Models;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class UMCViewModel
    {
        public User user {get; set;}

        public List<Message> messages {get; set;}

        public List<Comment> comments {get; set;}
    }
}