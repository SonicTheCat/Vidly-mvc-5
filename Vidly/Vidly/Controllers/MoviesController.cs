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

        public ActionResult Details(int id)
        {
            var movie = this.context
                .Movies
                .Include(x => x.Genre)
                .SingleOrDefault(x => x.Id == id);

            return this.View(movie);
        }

        public ActionResult Create()
        {
            var viewModel = new CreateMovieViewModel
            {
                Genres = this.context.Genres.ToList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            movie.DateAdded = DateTime.Now;
            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            return this.RedirectToAction(nameof(this.Index));
        }

        public ActionResult Edit(int id)
        {
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
        public ActionResult Edit(Movie movie)
        {
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
    }
}