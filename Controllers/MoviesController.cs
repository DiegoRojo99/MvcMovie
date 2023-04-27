using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace McvMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre,string searchString, string movieRating, string moviePrice)
        {
            if(_context.Movie == null){
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }

            IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre;
            IQueryable<string> ratingQuery = from m in _context.Movie orderby m.Rating select m.Rating;
            var movies = from m in _context.Movie select m;

            if(!String.IsNullOrEmpty(searchString)){
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }
            if(!String.IsNullOrEmpty(movieGenre)){
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            if(!String.IsNullOrEmpty(movieRating)){
                movies = movies.Where(x => x.Rating == movieRating);
            }
            if(!String.IsNullOrEmpty(moviePrice)){
                movies = movies.Where(x => x.Rating == moviePrice);
            }
            var movieGenreVM = new MovieGenreViewModel{
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Ratings = new SelectList(await ratingQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };
            
            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
            {
                return NotFound();
            }

            var stars= _context.Star.Where(s => s.MovieId == movie.Id);
            var actors = _context.Actor;
            foreach(var star in stars)
            {
                foreach(var actor in actors)
                { 
                    if(actor.Id.Equals(star.ActorId)){  
                        movie.Stars.Add(star);
                    }
                }
            } 
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (!id.Equals(movie.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
          return (_context.Movie?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
