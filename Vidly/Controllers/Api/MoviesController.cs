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


        public IEnumerable<MovieDto> GetMovies()
        {
            //Maps Customer to CustomerDto with delegate method reference
            return _context.Movies
                .Include(x => x.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        ////GET /api/movies
        //public IHttpActionResult GetMovies()
        //{
        //    return Ok(_context.Movies.Include(x => x.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>));
        //}

        //GET /api/movies/1 <-- Id
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
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
        public void PutMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);

            Mapper.Map(movieDto, movie);
        }

        [HttpDelete]
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