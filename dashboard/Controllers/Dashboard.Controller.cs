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

    public class DashboardController : Controller
    {
        private User ActiveUser  //private property for this class (WeddingController) ONLY!
        {
            //get{ return _context.user.Where(u => u.user_id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
             get{ return _context.users.FirstOrDefault(u => u.user_id == HttpContext.Session.GetInt32("id"));}  // either way works. Will return all info relating to the signed in user
        }
        private MyContext _context;
        public DashboardController(MyContext context)
        {
            _context = context;
        }

     
        [HttpGet("dashboard")]
        public IActionResult Guest()
        {
           var users = _context.users.ToList();
            
            return View(users);
        }

        [HttpGet("dashboard/admin")]
        public IActionResult Admin()
        {
            IndexView model = new IndexView();
            
                model.Users = new List<User>();
                model.Users = _context.users.ToList();
                model.Current_User = new User();
                model.Current_User.user_id = (int)HttpContext.Session.GetInt32("id");

                /*List<User> users = new List<User>();

                users = _context.users.ToList();*/
            
            return View(model);
        }

        [HttpGet("dashboard/add_user")]
        public IActionResult Add_User()
            {
                User current_user = new User();
                current_user.user_id = (int)HttpContext.Session.GetInt32("id");   
                
                IndexView model = new IndexView();
            
                model.Users = new List<User>();
                model.Users = _context.users.ToList();
                model.Current_User = _context.users.SingleOrDefault(user => user == current_user);
                

                return View(model);
            
            }

            [HttpPost("dashboard/add_user")]
            public IActionResult CreateMessage(IndexView model)
            {
                Message message = model.MessageUser;
                message.message = model.MessageUser.message;
                message.user_id = model.Current_User.user_id;
                _context.messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction("GuestUserInfo", "Home", new {id= model.MessageUser.user_id});
            
            }

        
        
    
    }


}