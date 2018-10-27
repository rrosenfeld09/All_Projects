using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace budget.Controllers
{
    public class BaseEntity : Controller 
    {
        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}