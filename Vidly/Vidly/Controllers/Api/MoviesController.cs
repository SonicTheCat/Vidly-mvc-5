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
using System.Data.Entity;

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
        public IHttpActionResult GetMovies(string query)
        {
            var moviesQuery = this.context
                .Movies
                .Include(x => x.Genre)
                .Where(x => x.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(x => x.Name.Contains(query));
            }

            var moviesDto = moviesQuery.ToList()
            .Select(Mapper.Map<Movie, MovieDto>);

            return this.Ok(moviesDto);
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
        public IHttpActionResult UpdateMovie(int id, MovieDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var movieInDb = this.context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null)
            {
                return this.NotFound();
            }

            Mapper.Map(dto, movieInDb);
            this.context.SaveChanges();

            return this.Ok();
        }

        //DELETE /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = this.context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null)
            {
                return this.NotFound();
            }

            this.context.Movies.Remove(movieInDb);
            this.context.SaveChanges();

            return this.Ok();
        }
    }
}