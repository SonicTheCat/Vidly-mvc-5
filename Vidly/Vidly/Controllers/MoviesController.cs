using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using Vidly.Constants;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext context;

        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = this.context
                .Movies
                .Include(x => x.Genre)
                .ToList();

            return View(movies);
        }

        [Authorize(Roles = WebConstants.CanManageCustomersRole)]
        public ActionResult Create()
        {
            var viewModel = new CreateMovieViewModel
            {
                Genres = this.context.Genres.ToList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.CanManageCustomersRole)]
        public ActionResult Create(Movie movie)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateMovieViewModel
                {
                    Movie = movie,
                    Genres = this.context.Genres.ToList()
                };

                return this.View(viewModel);
            }

            movie.DateAdded = DateTime.Now;
            movie.NumberAvailable = movie.NumberInStock; 
            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            return this.RedirectToAction(nameof(this.Index));
        }

        public ActionResult Edit(int id)
        {
            if (!this.User.IsInRole(WebConstants.CanManageMoviesRole))
            {
                return this.RedirectToAction(nameof(this.Details), new { id = id });
            }

            var movie = this.context
               .Movies
               .Include(x => x.Genre)
               .SingleOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new EditMovieViewModel
            {
                Movie = movie,
                Genres = this.context.Genres.ToList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (!this.User.IsInRole(WebConstants.CanManageMoviesRole))
            {
                return this.RedirectToAction(nameof(this.Details));
            }

            if (!this.ModelState.IsValid)
            {
                var viewModel = new EditMovieViewModel
                {
                    Movie = movie,
                    Genres = this.context.Genres.ToList()
                };

                return this.View(viewModel);
            }

            var movieFromDb = this.context
                .Movies
                .Single(x => x.Id == movie.Id);

            movieFromDb.Name = movie.Name;
            movieFromDb.ReleaseDate = movie.ReleaseDate;
            movieFromDb.GenreId = movie.GenreId;
            movieFromDb.NumberInStock = movie.NumberInStock;

            this.context.SaveChanges();
            return this.RedirectToAction(nameof(this.Index));
        }

        public ActionResult Details(int id)
        {
            var movie = this.context
                .Movies
                .Include(x => x.Genre)
                .SingleOrDefault(x => x.Id == id);

            return this.View(movie);
        }
    }
}