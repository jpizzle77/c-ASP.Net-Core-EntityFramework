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
        public WeddingController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
           
            return View();
        }
        
        [HttpGet("create_wedding")]
        public IActionResult CreateWedding()
        {
           
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateWedding(Wedding wedding)
        {
           
            return View();
        }


    }
}