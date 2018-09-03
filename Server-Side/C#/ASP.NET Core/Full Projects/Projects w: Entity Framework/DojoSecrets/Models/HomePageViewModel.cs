using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace DojoSecrets.Models
{
    public class HomePageViewModel
    {
        public User user {get; set;}

        public List<Secret> secrets {get; set;}

        public List<Like> likes {get; set;}

        public Secret secret {get; set;}
    }
}