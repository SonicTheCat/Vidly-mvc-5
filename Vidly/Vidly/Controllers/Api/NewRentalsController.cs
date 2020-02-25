using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext context;

        public NewRentalsController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            // We dont need to do all these validation for our current app. 
            // If we create a public API, which will be used from 
            // many other apps - we must cover all the edge cases and 
            // provide meaningful messages.
            // For now we can leave only the last validation,
            // where we check if the chosen movie is available.
            //if (newRental.MovieIds.Count == 0)
            //{
            //    return this.BadRequest("No MovieIds have been provided.");
            //}

            var customerInDb = this.context
                .Customers
                .SingleOrDefault(x => x.Id == newRental.CustomerId);

            //if (customerInDb == null)
            //{
            //    return this.BadRequest("CustomerId is not valid."); 
            //}

            var movies = this.context
                .Movies
                .Where(x => newRental.MovieIds.Contains(x.Id))
                .ToList();

            //if (movies.Count != newRental.MovieIds.Count)
            //{
            //    return this.BadRequest("One or more MovieIds are invalid."); 
            //}

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return this.BadRequest("Movie is not available."); 
                }

                var rental = new Rental()
                {
                    Customer = customerInDb,
                    Movie = movie,
                    DateRented = DateTime.Now,                   
                };

                this.context.Rentals.Add(rental);
                movie.NumberAvailable--;
            }

            this.context.SaveChanges();

            return this.Ok(); 
        }
    }
}