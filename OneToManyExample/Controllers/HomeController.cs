using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OneToManyExample.Models;

namespace OneToManyExample.Controllers
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
            ViewBag.Artists = _context.artists.ToList();
            return View();
        }

        [HttpGet("artist/{id}")]
        public IActionResult Show(int id)
        {
           // query search to select the artist that was clicked on
            Artist thisArtist = _context.artists
                                // include the List<Album> reference in the model Album
                                .Include(artist => artist.Albums)
                                .SingleOrDefault(artist => artist.artist_id == id);

            //create a new IndexView that will store the artist info
            // also create an new album (eliminates null errors)
            IndexView model = new IndexView()
            {
                newArtist = thisArtist,
                newAlbum = new Album()
            };
            
            return View(model);
        }


        //************************************************************* */
        [HttpPost("create_artist")]
        public IActionResult CreateArtist(IndexView model)
        {
            Artist new_artist = model.newArtist;
            // Check Uniqueness of artist name
            if(_context.artists.Where(artist => artist.name == new_artist.name) // IF...  For each artist in artists, 
                                                                                // Where artist.name = new_artist.name
                                                                                // will return either null or a match
                               .ToList() // convert to a list object, which will allow you to use the Count() method
                               .Count() > 0) // use count method to evaluate it as a boolean
            {
                ModelState.AddModelError("name", "Artist name already exists");

            }


             if(ModelState.IsValid)
            { 
               _context.artists.Add(new_artist); // here you are adding the new object 'person' to the artists table in mysql
               _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost("create_album")]
        public IActionResult CreateAlbum(IndexView model)
        {
            Album new_album = model.newAlbum;
            model.newArtist =_context.artists.SingleOrDefault(artist => artist.artist_id == new_album.artist_id);
            //Artist new_artist = model.newArtist;
            //return Json(model);
          if(ModelState.IsValid)
            { 
               _context.albums.Add(new_album); // here you are adding the new object 'person' to the artists table in mysql
               _context.SaveChanges();
                return RedirectToAction("Show", new {id=new_album.artist_id});
            }
          
            return View("Show", model);
        
        }

        
    }
}
//new {id=new_album.artist_id}