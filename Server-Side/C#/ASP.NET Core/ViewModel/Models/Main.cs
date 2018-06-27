using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ViewModel.Models
{
    public class User
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
    }

    public class Message
    {
        public string MessageContent {get; set;}
    }

    public class Numbers
    {
        public List<int> nums {get; set;}

        public Numbers()
        {
            nums = new List<int>();
        }
    }

    public class Users
    {
        public List<string> users {get; set;}

        public Users()
        {
            users = new List<string>();
        }
    }
}