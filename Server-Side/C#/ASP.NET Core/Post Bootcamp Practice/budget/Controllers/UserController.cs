using System;
using System.Collections.Generic;
using System.Linq;
using budget.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace budget.Controllers
{
    public class UserController : BaseEntity
    {
        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("HomePage");
            }
            else 
            {
                return View();
            }
        }


        [HttpPost("newuser/post")]
        public IActionResult CreateUser(IndexViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                if(submittedUser.user.password == submittedUser.user.confirm_pw)
                {
                    List<User> listOfUsers = _context.users
                                                    .Where(p => p.email == submittedUser.user.email)
                                                    .ToList();
                    if(listOfUsers.Count == 0)
                    {
                        //hash password and save to DB
                        PasswordHasher<User> Hasher = new PasswordHasher<User>();
                        submittedUser.user.password = Hasher.HashPassword(submittedUser.user, submittedUser.user.password);
                        _context.users.Add(submittedUser.user);
                        _context.SaveChanges();

                        //call Twilio API to send text string
                        SendText(5405882045, $"NEW USER REGISTERED: Name: {submittedUser.user.name}, Email: {submittedUser.user.email}, Time Registered: {DateTime.Now}");


                        //save user in session
                        User returnedUser = _context.users.Where(p => p.email == submittedUser.user.email)
                                                            .FirstOrDefault();

                        HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);

                        return RedirectToAction("CreateProfileProperties", "Goal");

                    }
                    TempData["error"] = "Already registered, please sign in";
                    return View("Index");
                }
                TempData["error"] = "Passwords don't match";
            }
            return View("Index");
        }

        [HttpPost("login/post")]
        public IActionResult Login(IndexViewModel submittedUser)
        {
            if(ModelState.IsValid)
            {
                User userToCheck = _context.users.Where(p => p.email == submittedUser.loginUser.email).FirstOrDefault();

                if(userToCheck == null)
                {
                    TempData["loginerror"] = "Email not registered yet. Please register above.";
                    return View("Index");
                }

                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(userToCheck, userToCheck.password, submittedUser.loginUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", userToCheck.user_id);
                    Console.WriteLine($"Successfully logged in user_id {userToCheck.user_id}");
                    return RedirectToAction("HomePage");
                }
                else 
                {
                    TempData["loginerror"] = "Incorrect password";
                }
            }
            
            return View("Index");
        }
        [HttpGet("homepage/get")]
        public IActionResult HomePage()
        {
            if(IsUserInSession())
            {
                HomePageViewModel viewModel = new HomePageViewModel();
                viewModel.user = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                viewModel.goal = _context.goals.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("user/{user_id}/edit")]
        public IActionResult EditUser(int user_id)
        {
            if(IsUserInSession())
            {
                if(user_id != HttpContext.Session.GetInt32("loggedUser"))
                {
                    return RedirectToAction("Homepage");
                }
                User returnedUser = _context.users.Where(p => p.user_id == HttpContext.Session.GetInt32("loggedUser")).FirstOrDefault();
                return View("edituser", returnedUser);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("user/{user_id}/update")]
        public IActionResult UpdateUserInfo(int user_id, string name, string email, string phone)
        {
            if(IsUserInSession())
            {
                if(user_id == HttpContext.Session.GetInt32("loggedUser"))
                {
                    if(name != null || email != null || phone != null)
                    {
                        User userToUpdate = _context.users.Where(p => p.user_id == user_id).FirstOrDefault();
                        if(name != null)
                        {
                            userToUpdate.name = name;
                        }
                        if(email != null)
                        {
                            userToUpdate.email = email;
                        }
                        if(phone != null)
                        {
                            userToUpdate.phone = phone;
                        }
                        _context.SaveChanges();
                        SendText(Int64.Parse(userToUpdate.phone), "Looks like you changed some of your account info. If you didn't authorize this, please login");

                        return RedirectToAction("Homepage");
                    }
                }
                return RedirectToAction("EditUser", new {user_id = HttpContext.Session.GetInt32("loggedUser")});
            }
            return RedirectToAction("Index");
        }

        [HttpGet("resetpassword/get")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost("getresetcode")]
        public IActionResult GetPasswordCode(string email)
        {
            if(email != null)
            {
                User returnedUser = _context.users.Where(p => p.email == email).FirstOrDefault();
                if(returnedUser != null)
                {
                    //generate random alphanumeric string
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var randomString = new char[8];
                    Random rand = new Random();

                    for(int i = 0; i < randomString.Length; i++)
                    {
                        randomString[i] = chars[rand.Next(chars.Length)];
                    }

                    string resetCode = new string(randomString);


                    //convert string phone number to int
                    Int64 phone_num = Int64.Parse(returnedUser.phone);
                    SendText(phone_num, $"Hello, {returnedUser.name}! Your password reset code is: {resetCode}");

                    //save reset code to DB
                    PasswordResetCode newCode = new PasswordResetCode();
                    newCode.user_id = returnedUser.user_id;
                    newCode.code = resetCode;
                    _context.password_reset_codes.Add(newCode);
                    _context.SaveChanges();

                    return RedirectToAction("ConfirmResetCode", new {user_id = returnedUser.user_id});
                }
                Console.WriteLine("User not found");
            }
            return RedirectToAction("ResetPassword");
        }

        [HttpGet("confirmcode/get")]
        public IActionResult ConfirmResetCode(int user_id)
        {
            return View();
        }

        [HttpPost("resetpassword/post")]
        public IActionResult ResetPasswordAction(string email, string code, string password, string confirm_pw)
        {
            if(email != null && code != null && password != null && confirm_pw != null)
            {
                Console.WriteLine("we're in");
                if(password == confirm_pw)
                {
                    User returnedUser = _context.users.Where(p => p.email == email).FirstOrDefault();
                    if(returnedUser != null)
                    {
                        PasswordResetCode codeToMatch = _context.password_reset_codes.Where(p => p.user_id == returnedUser.user_id)
                                                                                    .OrderByDescending(p => p.created_at)
                                                                                    .Take(1)
                                                                                    .FirstOrDefault();
                        if(code == codeToMatch.code)
                        {
                            //hash and save new password
                            PasswordHasher<User> Hasher = new PasswordHasher<User>();
                            password = Hasher.HashPassword(returnedUser, password);
                            returnedUser.password = password;
                            _context.Update(returnedUser);
                            _context.SaveChanges();

                            //log user in
                            HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                            return RedirectToAction("Homepage");
                        }                                                        
                    }
                    TempData["error"] = "Email not registered";
                    return View("confirmresetcode");
                }
                TempData["error"] = "Passwords dont match";
                return View("confirmresetcode");
            }
            TempData["error"] = "Can't be blank";
            return View("confirmresetcode");
        }

        [HttpGet("user/{user_id}/delete")]
        public IActionResult DeleteUser(int user_id)
        {
            if(user_id == HttpContext.Session.GetInt32("loggedUser"))
            {
                User returnedUser = _context.users.Where(p => p.user_id == user_id).FirstOrDefault();
                _context.Remove(returnedUser);
                _context.SaveChanges();
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}