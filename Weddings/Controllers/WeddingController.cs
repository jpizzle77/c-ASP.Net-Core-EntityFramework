using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Weddings.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Weddings.Controllers
{
    public class WeddingController : Controller
    {
        private MyContext _context;
        private User ActiveUser  //private property for this class (WeddingController) ONLY!
        {
            //get{ return _context.user.Where(u => u.user_id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
             get{ return _context.user.FirstOrDefault(u => u.user_id == HttpContext.Session.GetInt32("id"));}  // either way works. Will return all info relating to the signed in user
        }
        public WeddingController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
           IndexView model = new IndexView()
           {
                Weddings = _context.weddings
                          .Include(wedding => wedding.wedding_rsvps) //will include a list of rsvps for every wedding 
                            .ThenInclude(rsvp => rsvp.UserRsvp) // will give access to to every rsvp user info
                          .ToList(),
                
                Current_User = ActiveUser
                
           };

        return View(model);
        }
        
        [HttpGet("create_wedding")]
        public IActionResult CreateWedding()
        {
            IndexView model = new IndexView();
            model.Wedding = new Wedding();
            model.Wedding.user_id = (int)HttpContext.Session.GetInt32("id");
           // model.Wedding.wedding_rsvps = new List<Rsvp>(); //NOTE: setting this will 
            // let me delete the wedding later (you can do it here or when you post the weeding down below)
       
            return View(model);
        }

        [HttpGet("wedding/{id}")]
        public IActionResult ShowWeddingInfo(int id)
        {
            if(HttpContext.Session.GetInt32("id") == null)
               return RedirectToAction("Index");

            Wedding wedding_info = _context.weddings
                                    .Include(wedding => wedding.wedding_rsvps) // will give access to a list of reservations for this particular wedding. For each wedding, include the wedding's rsvps
                                        .ThenInclude(rsvp =>rsvp.UserRsvp) // will give access to user info (from User class). For each rsvp, include the rsvp user_info
                                    .Where(wedding => wedding.wedding_id == id) //this narrows it down to the wedding that was selected
                                    .SingleOrDefault();
            //the wedding_info object is for 1 wedding, that will have all the reservations for 
            // it, along with all the user info for person who made the reservation
            return View(wedding_info);
            
        }

        [HttpPost("create")]
        public IActionResult CreateWedding(IndexView model)
        {
            Wedding wedding = model.Wedding;  

            if(ModelState.IsValid)
            {
                Wedding new_wedding = new Wedding() // create new Wedding object called 'new_wedding' to store incoming data
                    {
                        user_id = wedding.user_id,
                        wedder_one = wedding.wedder_one,
                        wedder_two = wedding.wedder_two,
                        date_of_wedding = wedding.date_of_wedding,
                        wedding_address = wedding.wedding_address,
                        wedding_rsvps = new List<Rsvp>()
                         //NOTE: setting wedding_rsvps will let me delete the wedding later 

                    };
                _context.weddings.Add(new_wedding); // add new wedding object to database
                _context.SaveChanges(); // saves the changes (where the object will officially get a wedding_id)
            
                return RedirectToAction("Index");
            }
            return View(model);
           
        }

        [HttpGet("rsvp/{id}")]
        public IActionResult Rsvp(int id) // id = the wedding_id that was clicked on Index.cshtml page
        {
    
            Rsvp new_rsvp = new Rsvp() //creating a new Rsvp object
            {
                  user_id = (int)HttpContext.Session.GetInt32("id"),
                  wedding_id = id
            };
          
          
            _context.rsvps.Add(new_rsvp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("un_rsvp/{id}")]
        public IActionResult UnRsvp(int id) // id = the wedding_id that was clicked on Index.cshtml page
        {
            
            Rsvp delete_rsvp = _context.rsvps
                                .Where(rsvp => rsvp.wedding_id == id) // selecting one wedding
                                .Where(rsvp => rsvp.user_id == ActiveUser.user_id) //selecting the user who made the rsvp
                                .SingleOrDefault();
            
            _context.rsvps.Remove(delete_rsvp);
            _context.SaveChanges();
           
          
            return RedirectToAction("Index");
        }


        [HttpGet("delete/{id}")]
        public IActionResult DeleteWedding(int id)
        {
            Wedding delete_wedding = _context.weddings
                                .Where(wedding => wedding.user_id == ActiveUser.user_id)
                                .Where(wedding => wedding.wedding_id == id)
                                .SingleOrDefault();
            
            _context.weddings.Remove(delete_wedding);
            _context.SaveChanges();
           
          
            return RedirectToAction("Index");
        }

        [HttpGet("api_map")]
        public IActionResult API()
        {
           
          
            return View();
        }



    }
}