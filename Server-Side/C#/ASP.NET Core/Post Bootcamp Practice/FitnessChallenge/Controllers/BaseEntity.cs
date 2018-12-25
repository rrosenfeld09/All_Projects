using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessChallenge.Controllers
{
    public class BaseEntity : Controller
    {
        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") != null)
            {
                return true;
            }
            return false;
        }
    }
}