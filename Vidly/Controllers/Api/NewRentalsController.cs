using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.SingleOrDefault(
                c => c.Id == newRental.CustomerId);

            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie Ids have been given.");

            if (customer == null)
                return BadRequest("Customer Id Is not Valid.");

            var movies = _context.Movies.Where(
                x => newRental.MovieIds.Contains(x.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more MovieIds are invalid.");

            var dateRented = DateTime.Now;

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                //Decrement NumberAvailable
                movie.NumberAvailable--;
                var rental = new Rental()
                {
                    Movie = movie,
                    Customer = customer,
                    DateRented = dateRented,                  
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();

            //Not creating a single new object, creating multiple resources
           // return Created(new Uri(Request.RequestUri + "/" + customer.Id), newRental);
        }

    }
}
