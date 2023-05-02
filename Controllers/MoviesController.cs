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
        public async Task<IActionResult> Index(string movieGenre,string searchString, string movieRating)
        {
            if(_context.Movie == null){
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }

            IQueryable<Guid?> genreQuery = from m in _context.Movie orderby m.GenreId select m.GenreId;
            IQueryable<Guid?> ratingQuery = from m in _context.Movie orderby m.RatingId select m.RatingId;
            var movies = from m in _context.Movie select m;

            if(!String.IsNullOrEmpty(searchString)){
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }
            if(!String.IsNullOrEmpty(movieGenre)){
                movies = movies.Where(m => m.GenreId.Equals(Guid.Parse(movieGenre)));
            }
            if(!String.IsNullOrEmpty(movieRating)){
                movies = movies.Where(m => m.RatingId.Equals(Guid.Parse(movieRating)));
            }
            var movieGenreVM = new MovieGenreViewModel{
                Genres = new SelectList(_context.Genre,"Id","Name"),
                Ratings = new SelectList(_context.Rating,"Id","Name"),
                Movies = await movies.ToListAsync()
            };
            
            
            foreach (var g in _context.Genre)
            {
                foreach (var m in movies)
                {
                    if(m.GenreId.Equals(g.Id)){
                        m.Genre=g;
                    }
                }
            } 
            foreach (var r in _context.Rating)
            {
                foreach (var m in movies)
                {
                    if(m.RatingId.Equals(r.Id)){
                        m.Rating=r;
                    }
                }
            }       

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
            
            foreach(var g in _context.Genre)
            {
                if(g.Id.Equals(movie.GenreId)){  
                    movie.Genre=g;
                }
            } 
            foreach(var r in _context.Rating)
            {
                if(r.Id.Equals(movie.RatingId)){  
                    movie.Rating=r;
                }
            } 
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "Name");
            ViewBag.Ratings = new SelectList(_context.Rating, "Id", "Name");
            ViewBag.Stars = new MultiSelectList(_context.Actor, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,GenreId,Price,RatingId")] Movie movie, List<Guid> Stars)
        {
            if (ModelState.IsValid)
            {
                Guid g = Guid.NewGuid();
                movie.Id=g;
                _context.Add(movie);
                if(Stars!=null){
                    foreach (var item in Stars)
                    {
                        Star s = new Star();
                        s.MovieId=g;
                        s.ActorId=item;
                        _context.Add(s);
                    }
                }
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
            
            var allStars=_context.Star.Where(s=> s.MovieId.Equals(id));
            List<Guid> actorIDs = new List<Guid>();

            foreach (var star in allStars)
            {
                if(star.MovieId!=null){
                    actorIDs.Add((Guid)star.MovieId);
                }
            }

            ViewBag.Genres = new SelectList(_context.Genre, "Id", "Name");
            ViewBag.Ratings = new SelectList(_context.Rating, "Id", "Name");
            ViewBag.Stars = new MultiSelectList(_context.Actor, "Id", "Name",actorIDs);

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,ReleaseDate,GenreId,Price,RatingId")] Movie movie, List<Guid> Stars)
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
                     if(Stars!=null){
                    foreach (var item in Stars)
                        {
                            Star s = new Star();
                            s.MovieId=id;
                            s.ActorId=item;
                            _context.Add(s);
                        }
                    }
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
            
            foreach(var g in _context.Genre)
            {
                if(g.Id.Equals(movie.GenreId)){  
                    movie.Genre=g;
                }
            } 
            foreach(var r in _context.Rating)
            {
                if(r.Id.Equals(movie.RatingId)){  
                    movie.Rating=r;
                }
            } 

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                var starMatches = _context.Star.Where(star => star.MovieId == id);
                foreach (var item in starMatches)
                {
                    _context.Star.Remove(item);
                }
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
