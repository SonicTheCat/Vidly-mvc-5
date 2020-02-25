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
            var customerInDb = this.context
                .Customers
                .FirstOrDefault(x => x.Id == newRental.CustomerId);

            var movies = this.context
                .Movies
                .Where(x => newRental.MovieIds.Contains(x.Id))
                .ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available."); 
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