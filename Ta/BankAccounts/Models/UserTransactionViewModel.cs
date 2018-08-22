using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using BankAccounts.Models;
using System.Collections.Generic;

namespace BankAccounts.Models
{
    public class UserTransactionViewModel 
    {
        public List<Transaction> tranList {get; set;}

        public User user {get; set;}

        //use this to pass a collection of things to the view
    }
}