using System;
using System.Linq;
using BankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        

        [HttpGet("")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(IndexView model)
        {
        
           
        //check databse if email already exists
            if(_context.users.Where(u => u.email == model.NewUser.email)
                                                  .ToList()
                                                  .Count() > 0) 
            // or you can write .....  if(_context.users.SingleOrDefault(u => u.email == model.NewUser.email) != null)
            {
                ModelState.AddModelError("NewUser.email", "Email already exists");
            }
           
            if(ModelState.IsValid) // No existing email was found. Create new User object and store incoming form info
            { 
                User user = new User()
                {
                    first_name = model.NewUser.first_name,
                    last_name = model.NewUser.last_name,
                    email = model.NewUser.email,
                    password = model.NewUser.password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                //hash plain text password
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashed = hasher.HashPassword(user, user.password);
                user.password = hashed;
               _context.users.Add(user);
               _context.SaveChanges();
              HttpContext.Session.SetInt32("id",(int)user.user_id);
              int registered_user_id = (int)HttpContext.Session.GetInt32("id");

              return RedirectToAction("Index","Transaction1", new { id = registered_user_id });
            }
            
    
            return View("Index");
            
        }
        [HttpPost("login")]
        public IActionResult Login(IndexView model)
        {
            
            LoginUser user = model.LogUser;

            if(ModelState.IsValid)
             {
                //check that email is in the database
                if(_context.users.Where(u => u.email == model.LogUser.log_email)
                                                  .ToList()
                                                  .Count() == 0)   
                // or ...... if(_context.users.SingleOrDefault(u => u.email == model.NewUser.email) == null)
                    {
                        ModelState.AddModelError("LogUser.log_email", "invalid email or password 1"); 
                    }

            //check hashed password if email exists in database
                   
                else
                {
                    //retrieve the password from the person that is logging in with a valid email
                    User checkPassword = _context.users.SingleOrDefault(u => u.email == model.LogUser.log_email);

                    // create PasswordHasher object to compare incoming form password with password stored in DB
                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>(); 

                    // the VerifyHashedPassword method will return a 0 (False) or 1 (true) to let you know if the passwords matched
                    if(hasher.VerifyHashedPassword(user, checkPassword.password, user.log_password) == 0)
                    {
                        ModelState.AddModelError("LogUser.log_password", "invalid email or password 2");   //password failed
                    }
                                    
                    //query the user
                    
                    User user1 = _context.users.SingleOrDefault(person => person.email == model.LogUser.log_email);
                    HttpContext.Session.SetInt32("id",(int)user1.user_id);
                    int logged_in_user_id = (int)HttpContext.Session.GetInt32("id");
                   
                    return RedirectToAction("Index","Transaction1", new { id = logged_in_user_id });

                }
             }
           
           
            return View("Index");
        }

        
        }
    }

