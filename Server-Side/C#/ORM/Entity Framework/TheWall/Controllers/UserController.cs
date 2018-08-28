using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;



namespace TheWall.Controllers
{
    public class UserController : Controller
    {
        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        public bool IsUserInSession()
        {
            int? loggedUser = HttpContext.Session.GetInt32("loggedUser");
            if(loggedUser == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }

        [HttpPost("register_user")]
        public IActionResult RegisterUser(User submittedUser)
        {
            if(IsUserInSession())
            {
                return RedirectToAction("HomePage");
            }
            if(ModelState.IsValid)
            {
                if(_context.users.Any(p => p.email == submittedUser.email))
                {
                    TempData["Error"] = "That email is already in use.";
                    return RedirectToAction("Index");
                }
                if(submittedUser.password != submittedUser.confirm_password)
                {
                    TempData["Error"] = "Your passwords don't match";
                    return RedirectToAction("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                submittedUser.password = Hasher.HashPassword(submittedUser, submittedUser.password);
                _context.Add(submittedUser);
                _context.SaveChanges();
                User returnedUser = _context.users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                return RedirectToAction("HomePage");
            }
            return View("Index");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }

        [HttpPost("login_action")]
        public IActionResult LoginAction(LoginUser submittedUser)
        {
            if(ModelState.IsValid)
            {
                User returnedUser = _context.users.Where(p => p.email == submittedUser.email).FirstOrDefault();
                if(returnedUser == null)
                {
                    TempData["Error"] = "This email is not registered.";
                    return RedirectToAction("Login");
                }
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(returnedUser, returnedUser.password, submittedUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                    return RedirectToAction("HomePage");
                }
                else
                {
                    TempData["Error"] = "Incorrect password";
                    return RedirectToAction("Login");
                }
            }
            return View("Login");
        }

        [HttpGet("homepage")]
        public IActionResult HomePage()
        {
            if (IsUserInSession() == false)
            {
                return RedirectToAction("Index");
            }

            //pull members for the UMCViewModel Class
            User user = _context.users.Where(p => p.user_id == (HttpContext.Session.GetInt32("loggedUser"))).FirstOrDefault();
            List<Message> messages = _context.messages.OrderByDescending(p => p.created_at).Include(p => p.user).ToList();
            List<Comment> comments = _context.comments.Include(p => p.user).ToList();

            UMCViewModel viewModel = new UMCViewModel() {user = user, messages = messages, comments = comments};
            return View(viewModel);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}