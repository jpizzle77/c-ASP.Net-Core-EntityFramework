using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESTauranter.Models;


namespace RESTauranter.Controllers
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
            Review model = new Review();

            return View(model);
        }

        [HttpPost("create")]
        public IActionResult Create(Review review)
        {

            return Json(review);

            /*if(ModelState.IsValid)
            { 
                Review model = new Review()
                {
                    reviewer_name = review.reviewer_name,
                    restaurant_name = review.restaurant_name,
                    review = review.review,
                    date_of_visit =review.date_of_visit,
                    rating = review.rating 
                    
                };

               _context.restaurant_reviews.Add(model);
               _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");*/
          
        }

       
    }
}
