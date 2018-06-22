using Microsoft.AspNetCore.Mvc;
namespace PortfolioI
{
    public class MainController : Controller
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "This is my Index!";
        }

        [HttpGet]
        [Route("{item}")]
        public string Thing(string item)
        {
            return "These are my " + item;
        }

        [HttpGet]
        [Route("contact")]
        public string Contact()
        {
            return "These are my contacts!";
        }
    }
}