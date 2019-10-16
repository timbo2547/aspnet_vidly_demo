using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            //List<Movie> movies = new List<Movie>
            //{
            //    new Movie { Id = 1, Name = "Shrek"},
            //    new Movie { Id = 2, Name = "Ice Age"}
            //};

            //IndexMovieViewModel viewModel = new IndexMovieViewModel
            //{
            //    Movies = movies
            //};

            //var movies = _context.Movies.Include(x => x.Genre).ToList();
            return View();
        }

        public ActionResult Random()
        {
            Movie movie = new Movie() { Id = 1, Name = "Shrek" };

            List<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Wayne"},
                new Customer { Id = 2, Name = "Cuck Jones"}
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //return View(movie);
            return View(viewModel);
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            //List<Movie> movies = new List<Movie>
            //{
            //    new Movie { Id = 1, Name = "Shrek"},
            //    new Movie { Id = 2, Name = "Ice Age"}
            //};

            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(movie);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Used in conjunction with View Helper method AntiForgeryToken
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0) //Add
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else //Edit
            {
                var movieInDb = _context.Movies.Single(x => x.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;

                ////Optional 2:
                //TryUpdateModel(customerInDb);

                ////Optional 3 Automapper:
                //Mapper.Map(customer, customerInDb);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }




        //ASP.Net MVC Attribute Route Constraints
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        // GET: Movies
        //public ActionResult Random()
        //{




        //    var movie = new Movie() { Name = "Shrek!" };

        //    ////Add the movie object to the ViewData dictionary
        //    //ViewData["Movie"] = movie;

        //    //Add the movie to ViewBag
        //    //ViewBag.RandomMovie = movie;




        //    return View(movie);
        //    //return new ViewResult();
        //    //return Content("hello world");
        //    //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"} );


        //    //ActionResult Return types
        //    // View()
        //    /* PartialView() 
        //     * Content()
        //     * Redirect()
        //     * RedirectToAction()
        //     * Json()
        //     * File()
        //     * HttpNotFound()
        //     * return new EmptyResult();
        //     */

        //}


    }
}