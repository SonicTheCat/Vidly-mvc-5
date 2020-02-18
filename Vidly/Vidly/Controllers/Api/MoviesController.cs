using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext context;

        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var movies = this.context
                .Movies
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return this.Ok(movies);
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = this.context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return this.NotFound();
            }
            var dto = Mapper.Map<Movie, MovieDto>(movie);
            return this.Ok(dto);
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(dto);
            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            dto.Id = movie.Id;
            return this.Created(new Uri(Request.RequestUri + "/" + movie.Id), dto);
        }

        //PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var movieInDb = this.context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(dto, movieInDb);
            this.context.SaveChanges();
        }

        //DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = this.context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            this.context.Movies.Remove(movieInDb);
            this.context.SaveChanges();
        }
    }
}