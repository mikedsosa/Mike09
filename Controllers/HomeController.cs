using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mike09.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mike09.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MovieDbContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Request for Index page
        public IActionResult Index()
        {
            return View();
        }

        //Request for Podcasts page
        public IActionResult Podcasts()
        {
            return View();
        }

        //GET Request for Enter Movie page
        [HttpGet]
        public IActionResult EnterMovie()
        {
            return View();
        }

        //POST Request for Enter Movie page
        [HttpPost]
        public IActionResult EnterMovie(Movie m)
        {
            //make sure there are no errors
            if (ModelState.IsValid)
            {
                //add the movie to the database
                _context.Movies.Add(m);
                _context.SaveChanges();
                return View("Confirmation", m);
            }
            //send back the same page with validation summary if there are errors
            else
            {
                return View();
            }
        }

        //POST Request for Delete Movie route
        [HttpPost]
        public IActionResult DeleteMovie(int movieID)
        {
            //search for the movie in the context DB
            Movie movie = _context.Movies.Where(m => m.MovieID == movieID).FirstOrDefault();

            //store the title to be used later
            TempData["Title"] = movie.Title;

            //remove the movie from the DB and send them to the confirmation page
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return View("ConfirmationDeletion");
        }

        //POST request to edit movies. I did it in two parts because there's like a break
        //in the middle of the request, so in the first part I'm just passing the object
        //they selected to the EditMovie view
        [HttpPost]
        public IActionResult EditMoviePartOne(int movieID)
        {
            //search for the object in the DB
            Movie movie = _context.Movies.Where(m => m.MovieID == movieID).FirstOrDefault();

            //Save the object to the viewbag
            ViewBag.Movie = movie;

            //send the Edit Movie view along with the object selected
            return View("EditMovie", ViewBag.Movie);
        }

        //Second Post request when editing movies
        //this one takes the edited movie information and sends it to the DB
        [HttpPost]
        public IActionResult EditMoviePartTwo(Movie m)
        {
            if (ModelState.IsValid)
            {
                //take the title to be used in the confirmation page
                TempData["Title"] = m.Title;

                //update the movie info in the DB
                _context.Movies.Update(m);
                _context.SaveChanges();

                //Send the confirmation page
                return View("ConfirmationEdit");
            }

            //send back the same page with validation summary if there are errors
            else
            {
                //Save the object to the viewbag
                ViewBag.Movie = m;

                //send the Edit Movie view along with the object selected
                return View("EditMovie", ViewBag.Movie);
            }

        }

        //Get request for view movies page. Send in the DB to use
        public IActionResult ViewMovies()
        {
            return View(_context.Movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
