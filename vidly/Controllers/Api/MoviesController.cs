using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using vidly.Dtos;
using vidly.Models;

namespace vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {

            

            var moviesQuery =  _context.Movies
                .Include(m => m.Genre);

                if(!String.IsNullOrWhiteSpace(query))
                    moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

                return moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        //GET api/movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.Today;
            movie.NumberAvailable = movieDto.Stock;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);

        }

        //PUT api/movies/{id}
        [HttpPut]
        public void EditMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var existingMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (existingMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            movieDto.DateAdded = existingMovie.DateAdded;
            movieDto.NumberAvailable = movieDto.Stock - _context.Rentals.Count(r => r.Movie.Id == movieDto.Id);

            if(movieDto.NumberAvailable < 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(movieDto, existingMovie);

            _context.SaveChanges();

        }

        //DELETE api/movie/{id}
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var existingMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (existingMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(existingMovie);
            _context.SaveChanges();

        }
    }
}
