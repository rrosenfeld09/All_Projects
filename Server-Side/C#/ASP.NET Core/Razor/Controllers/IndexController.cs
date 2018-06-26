using Microsoft.AspNetCore.Mvc;

namespace Razor
{
    public class Index : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Test()
        {
            return View("Index");
        }
    }
}