using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using REST2.Models;


namespace REST2.Controllers
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

        [HttpGet("reviews")]
        public IActionResult Reviews()
        {
            ViewBag.Reviews = _context.reviews.ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Review model)
        {
            Console.WriteLine(Json(model));
            

            if(ModelState.IsValid)
            { 
                Review review = new Review()
                {
                    reviewer_name = model.reviewer_name,
                    restaurant_name = model.restaurant_name,
                    review = model.review,
                    date_of_visit =model.date_of_visit,
                    rating = model.rating 
                    //rating = (IEnumerable<int>)model.rating 
                    
                };

               _context.reviews.Add(review);
               _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            return View("Index");
          
        }

       
    }
}
