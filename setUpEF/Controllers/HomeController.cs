using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using setUpEF.Models;

namespace setUpEF.Controllers
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
            ViewBag.Users = _context.users.ToList();
           
            return View();
        }

     
        [HttpGet("new")]
        public IActionResult New()  //New(NewUser user) this will result in showing the model errors from get go
        {
            
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(NewUser model)
        {   
        
            //check databse if email already exists
            if(_context.users.SingleOrDefault(u => u.email == model.email) != null)
            {
                ModelState.AddModelError("email", "Email already exists");
            }
            if(ModelState.IsValid)
            { 
                User user = new User()
                {
                    first_name = model.first_name,
                    last_name = model.last_name,
                    email = model.email,
                    password = model.password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                //hash plain text password
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashed = hasher.HashPassword(user, user.password);
                user.password = hashed;
               _context.users.Add(user);
               _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("New");
        }


        // ************* USER/{ID} ROUTES  One Get and Oone Post route ************************
        [HttpGet("user/{id}")]
        public IActionResult Show(int id)
        {
            User thisUser = _context.users.SingleOrDefault(user => user.id ==id);
            return View(thisUser);
        }

        [HttpPost("user/{id}")]
        public IActionResult Update(User user)
        {
            if(ModelState.IsValid)
            {
                User RetrievedUser = _context.users.SingleOrDefault(u => u.id == user.id);
                RetrievedUser.first_name = user.first_name;
                RetrievedUser.last_name = user.last_name;
                RetrievedUser.email = user.email;
                RetrievedUser.password = user.password;

                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View("Show", new {id=user.id});
        }

        //******************** DELETE ROUTES ***************************/

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user_to_delete = _context.users.SingleOrDefault(u => u.id == id);

            return View(user_to_delete);
        }
        [HttpPost("delete/{id}")]
        public IActionResult DeleteUser(User user)
        {
            
            User user_to_delete = _context.users.SingleOrDefault(u => u.id == user.id);
            _context.users.Remove(user_to_delete);
            _context.SaveChanges();
            NewUser person = new NewUser();
            return RedirectToAction("Index");
            
        }

        

       
    }
}
