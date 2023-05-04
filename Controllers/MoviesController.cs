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
        public async Task<IActionResult> Index(string movieGenre, string movieRating, string movieStream ,string searchString, string directorString)
        {
            if(_context.Movie == null){
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }

            IQueryable<Guid?> genreQuery = from m in _context.Movie orderby m.GenreId select m.GenreId;
            IQueryable<Guid?> ratingQuery = from m in _context.Movie orderby m.RatingId select m.RatingId;
            var movies = from m in _context.Movie select m;
            var streams = from ms in _context.MovieStreaming select ms;

            if(!String.IsNullOrEmpty(searchString)){
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }
            if(!String.IsNullOrEmpty(movieGenre)){
                movies = movies.Where(m => m.GenreId.Equals(Guid.Parse(movieGenre)));
            }
            if(!String.IsNullOrEmpty(movieRating)){
                movies = movies.Where(m => m.RatingId.Equals(Guid.Parse(movieRating)));
            }
            if(!String.IsNullOrEmpty(movieStream)){
                streams = streams.Where(ms=> ms.StreamingServiceId.Equals(Guid.Parse(movieStream)));
                List<Guid?> movieIDs=new List<Guid?>();
                foreach (var item in streams)
                {
                    movieIDs.Add(item.MovieId);
                }
                movies = movies.Where(m => movieIDs.Contains(m.Id));
            }
            
            if(!String.IsNullOrEmpty(directorString)){
                movies = movies.Where(m => m.DirectorId.Equals(Guid.Parse(directorString)));
            }
            var movieGenreVM = new MovieGenreViewModel{
                Genres = new SelectList(_context.Genre,"Id","Name"),
                Streamings = new SelectList(_context.StreamingService,"Id","Name"),
                Ratings = new SelectList(_context.Rating,"Id","Name"),
                Directors = new SelectList(_context.Director,"Id","Name"),
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
                    if(actor.Id.Equals(star.ActorId) && movie!=null && movie.Stars!=null){  
                        movie.Stars.Add(star);
                    }
                }
            } 
            var streams= _context.MovieStreaming.Where(s => s.MovieId == movie.Id);
            var streamingServices = _context.StreamingService;
            foreach(var stream in streams)
            {
                foreach(var ss in streamingServices)
                { 
                    if(ss.Id.Equals(stream.StreamingServiceId) && movie!=null && movie.Streams!=null){  
                        movie.Streams.Add(stream);
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
            List<Guid?> listIds = new List<Guid?>();
            if(movie!=null && movie.Streams!=null){
                foreach (var item in movie.Streams)
                {
                    if(listIds.Contains(item.StreamingServiceId)){   
                        movie.Streams.Remove(item);
                    }else{
                        listIds.Add(item.StreamingServiceId);
                    }
                }
            }
            
            if(movie!=null && movie.DirectorId!=null){   
                var director = await _context.Director
                .FirstOrDefaultAsync(d => d.Id.Equals(movie.DirectorId));
                movie.Director=director;
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        { 
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "Name");
            ViewBag.Ratings = new SelectList(_context.Rating, "Id", "Name");
            ViewBag.Stars = new MultiSelectList(_context.Actor, "Id", "Name");
            ViewBag.Streams = new MultiSelectList(_context.StreamingService, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,GenreId,Price,RatingId")] Movie movie, List<Guid> Stars, List<Guid> Streams)
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
                if(Streams!=null){
                        foreach (var item in Streams)
                        {
                            MovieStreaming s = new MovieStreaming();
                            s.MovieId=g;
                            s.StreamingServiceId=item;
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

            ViewBag.Streams = new MultiSelectList(_context.StreamingService, "Id", "Name");
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,ReleaseDate,GenreId,Price,RatingId")] Movie movie, List<Guid> Stars, List<Guid> Streams)
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
                    if(Streams!=null){
                        foreach (var item in Streams)
                        {
                            MovieStreaming s = new MovieStreaming();
                            s.MovieId=id;
                            s.StreamingServiceId=item;
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
