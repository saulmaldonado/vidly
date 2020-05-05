using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Windows.Markup;
using vidly.Models;
using vidly.ViewModels;

namespace vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("movies/random")]
        public ActionResult Random()
        {
            var movie = _context.Movies.ToList()[new Random().Next(_context.Movies.ToList().Count)];

            return View(movie);
        }

        [Route("movies")]
        public ActionResult Movies()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View();
            return View("ReadOnlyList");
        }

        [Route("movies/details/{id}")]
        public ActionResult MovieById(int id)
        {
            var movie = _context.Movies.Include(m =>m.Genre).FirstOrDefault(m => m.Id == id);

            if (movie != null)
                return View(movie);
            return HttpNotFound();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [Route("movies/new")]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new NewMovieViewModel
            {
                Genres = genres
            };

            return View(viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [Route("movies/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();
            var viewModel = new NewMovieViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("New", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("New", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.Stock;

                _context.Movies.Add(movie);
            }
            else
            {
                var existingMovie = _context.Movies.Single(m => m.Id == movie.Id);

                existingMovie.Name = movie.Name;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.GenreId = movie.GenreId;
                existingMovie.Stock = movie.Stock;
            }

            _context.SaveChanges();

            return RedirectToAction("Movies", "Movies");
        }
    }
}