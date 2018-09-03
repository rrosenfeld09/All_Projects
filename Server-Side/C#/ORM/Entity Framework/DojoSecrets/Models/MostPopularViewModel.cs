using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DojoSecrets.Models
{
    public class MostPopularViewModel
    {
        public User user {get; set;}
        public List<Secret> secrets {get; set;}

        public List<Like> likes {get; set;}
    }
}