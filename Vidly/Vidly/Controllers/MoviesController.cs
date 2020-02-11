using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            IEnumerable<IndexMovieViewModels> models = new List<IndexMovieViewModels>()
            {
                new IndexMovieViewModels
                {
                    Id = 1,
                    Name = "Shrek"
                },
                new IndexMovieViewModels
                {
                    Id = 2,
                    Name = "Wall-e"
                }
            }; 

            return View(models);
        }



        //public ActionResult Random()
        //{
        //    var movie = new Movie { Name = "shrek!" };

        //    var result = new ViewResult();
        //    result.ViewData.Model = result; 

        //    return this.View(movie); 
        //}

        //public ActionResult ShowAll(string sortBy)
        //{
        //    return this.Content($"sortBy=" + sortBy); 
        //}

        //[Route("movies/released/{year}/{month}")]
        //public ActionResult ByReleasedDate(int year, int month)
        //{
        //    return this.Content(year + "/" + month); 
        //}
    }
}