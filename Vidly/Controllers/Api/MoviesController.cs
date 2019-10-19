using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetMovies(string query = null)
        {       
            var moviesQuery = _context.Movies
                .Include(x => x.Genre);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(x => x.Name.Contains(query) && x.NumberAvailable > 0);

            var movieDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        //public IHttpActionResult GetCustomers(string query = null)
        //{
        //    
        //    var customersQuery = _context.Customers
        //            .Include(x => x.MembershipType);

        //    if (!String.IsNullOrWhiteSpace(query))
        //        customersQuery = customersQuery.Where(c => c.Name.Contains(query));

        //    var customerDtos = customersQuery
        //        .ToList()
        //        .Select(Mapper.Map<Customer, Customer>);

        //    return Ok(customerDtos);
        //}


        ////GET /api/movies
        //public IHttpActionResult GetMovies()
        //{
        //    return Ok(_context.Movies.Include(x => x.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>));
        //}

        //GET /api/movies/1 <-- Id
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult PostMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/1 <-- Id)
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void PutMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);

            Mapper.Map(movieDto, movie);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}