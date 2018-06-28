using System;
using Microsoft.AspNetCore.Mvc;

namespace Dojo_Survey.Models
{
    public class User
    {
        public string name {get; set;}
        public string location {get; set;}
        public string language {get; set;}
        public string comment {get; set;}
    }
}