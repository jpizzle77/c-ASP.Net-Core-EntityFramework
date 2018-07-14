using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dashboard.Controllers
{

    public class HomeController : Controller
    {
         private User ActiveUser  //private property for this class 
        {
            //get{ return _context.user.Where(u => u.user_id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
             get{ return _context.users.FirstOrDefault(u => u.user_id == HttpContext.Session.GetInt32("id"));}  // either way works. Will return all info relating to the signed in user
        }
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
            // or you can write .....  if(_context.user.SingleOrDefault(u => u.email == model.NewUser.email) != null)
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

               // the first registered user will be granted admin rights. Every other new
               // registred user will be considered a guest unless the 1st admin (or subsequent
               // admins) give them admin rights. The 1st admin will be able to give
               // a new registered user admin rights, or create/ and edit a new user w admin rights

               if(user.user_id == 16) //this line of code will only be used on the 1st registration
               {
                   user.user_level = "Admin"; //changing user_level to admin
               }
               else
               {
                   user.user_level ="Guest";
               }
               _context.SaveChanges();
              HttpContext.Session.SetInt32("id",(int)user.user_id);

                // this will determine rather the user is an admin or a guest 
                if(user.user_level == "Admin")
                    {
                        
                        return RedirectToAction("Admin", "Dashboard");
                    }
                else
                    {
                        
                         return RedirectToAction("Guest", "Dashboard");
                    } 
                
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
                // or ...... if(_context.user.SingleOrDefault(u => u.email == model.NewUser.email) == null)
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
                                    
                    //log in user
                    User user1 = _context.users.SingleOrDefault(person => person.email == model.LogUser.log_email);
                    HttpContext.Session.SetInt32("id",(int)user1.user_id);
                   
                   // this will determine rather the user is an admin or a guest
                    if(user1.user_level == "Admin")
                        {
                            return RedirectToAction("Admin", "Dashboard");
                        }
                    else
                        {
                            return RedirectToAction("Guest", "Dashboard");
                        }

                }
             }
           
           
            return View("Index");
        }

        [HttpGet("users/edit/{id}")]
        public IActionResult ShowUserInfo(int id)
        {
            IndexView model = new IndexView();
            model.User = _context.users.SingleOrDefault(u => u.user_id == id);
            _context.SaveChanges();
            return View(model);
        }

        [HttpGet("users/show/{id}")]
        public IActionResult GuestUserInfo(int id)
        {
            IndexView model = new IndexView();
            model.User = _context.users.SingleOrDefault(u => u.user_id == id);
            model.Current_User = ActiveUser;
            
            _context.SaveChanges();

            
           
            return View(model);
        }

        [HttpPost("users/update/{id}")]
        public IActionResult UpdateUserInfo(User user, int id)
        {
            User update_user_info = _context.users.SingleOrDefault(u => u.user_id == id);

            //update_user_info.user_id = id;
            update_user_info.first_name = user.first_name;
            update_user_info.last_name = user.last_name;
            update_user_info.email = user.email;
            update_user_info.user_level = user.user_level;
            _context.SaveChanges();

            return RedirectToAction("Admin","Dashboard");
        }

        [HttpPost("users/update_password/{id}")]
        public IActionResult UpdatePasswordInfo(IndexView user, int id)
        {
            ConfirmUser confirm_new = user.NewUser;
            confirm_new.password = user.NewUser.password;

            if(confirm_new.password == confirm_new.confirm_password)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashed = hasher.HashPassword(confirm_new, confirm_new.password);
                
                User update_user_password = _context.users.SingleOrDefault(u => u.user_id == id);
                update_user_password.password = hashed;
                _context.SaveChanges();
                return RedirectToAction("Admin","Dashboard"); 
            }
            IndexView model = new IndexView();
            model.User = new User();
            model.User.user_id = user.NewUser.user_id;
            _context.SaveChanges();
            return View("ShowUserInfo", model);
            
        
        }


        [HttpPost("users/update_description/{id}")]
        public IActionResult UpdateUserDescription(IndexView model, int id)
        {
            User current_user = model.User;
            current_user = _context.users.SingleOrDefault(u => u.user_id == id);
            current_user.user_description = model.User.user_description;

            _context.SaveChanges();

            return RedirectToAction("Admin","Dashboard");
        }
       
        
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

       
    }
}
