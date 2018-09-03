using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace ViewModelFun.Models
{
    public class Users
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public Users(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
        }
    }

    public class UsersList
    {
        public List<Users> UList {get; set;}

        public UsersList(List<Users> arg)
        {
            UList = arg;
        }
    }

    public class Message
    {
        public string MessageContent {get; set;}
    }

    public class Numbers
    {
        public int[] NumberList {get; set;}

        public Numbers(int[] arr)
        {
            NumberList = arr;
        }
    }
}