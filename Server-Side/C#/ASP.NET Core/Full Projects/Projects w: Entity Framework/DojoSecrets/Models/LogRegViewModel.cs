using Microsoft.AspNetCore.Mvc;
using System;

namespace DojoSecrets.Models
{
    public class LogRegViewModel
    {
        public User user {get; set;}

        public LoginUser loginUser {get; set;}
    }
}