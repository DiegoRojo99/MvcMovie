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
    public class MovieStreamingController : Controller
    {
        private readonly MvcMovieContext _context;

        public MovieStreamingController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: MovieStreaming
        public async Task<IActionResult> Index(string countryCode, Guid movie)
        {
            if(_context.Movie == null){
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }
            var streams = from ms in _context.MovieStreaming select ms;
            IQueryable<string> countryCodeQuery = from s in _context.MovieStreaming orderby s.CountryCode select s.CountryCode;

            if(!String.IsNullOrEmpty(countryCode)){
                streams = streams.Where(s => s.CountryCode == countryCode);
            }
            if(!String.IsNullOrEmpty(movie.ToString())){
                streams = streams.Where(s => s.MovieId == movie);
            }
            var movieGenreVM = new MovieStreamingViewModel{
                Movies = new SelectList(_context.Movie,"Id","Title"),
                CountryCodes = new SelectList(await countryCodeQuery.Distinct().ToListAsync()),
                Streams = await streams.ToListAsync()
            };
            
            
            foreach (var ss in _context.StreamingService)
            {
                foreach (var s in streams)
                {
                    if(s.StreamingServiceId.Equals(ss.Id)){
                        s.StreamingService=ss;
                    }
                }
            }    
            
            return View(movieGenreVM);
        }

        // GET: MovieStreaming/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieStreaming == null)
            {
                return NotFound();
            }

            var movieStreaming = await _context.MovieStreaming
                .Include(m => m.Movie)
                .Include(m => m.StreamingService)
                .FirstOrDefaultAsync(m => m.MovieStreamingId == id);
            if (movieStreaming == null)
            {
                return NotFound();
            }

            return View(movieStreaming);
        }

        // GET: MovieStreaming/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title");
            ViewData["StreamingServiceId"] = new SelectList(_context.StreamingService, "Id", "Name");
            return View();
        }

        // POST: MovieStreaming/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieStreamingId,MovieId,StreamingServiceId,CountryCode")] MovieStreaming movieStreaming)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieStreaming);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", movieStreaming.MovieId);
            ViewData["StreamingServiceId"] = new SelectList(_context.StreamingService, "Id", "Name", movieStreaming.StreamingServiceId);
            return View(movieStreaming);
        }

        // GET: MovieStreaming/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieStreaming == null)
            {
                return NotFound();
            }

            var movieStreaming = await _context.MovieStreaming.FindAsync(id);
            if (movieStreaming == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", movieStreaming.MovieId);
            ViewData["StreamingServiceId"] = new SelectList(_context.StreamingService, "Id", "Name", movieStreaming.StreamingServiceId);
            return View(movieStreaming);
        }

        // POST: MovieStreaming/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieStreamingId,MovieId,StreamingServiceId,CountryCode")] MovieStreaming movieStreaming)
        {
            if (id != movieStreaming.MovieStreamingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieStreaming);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieStreamingExists(movieStreaming.MovieStreamingId))
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
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", movieStreaming.MovieId);
            ViewData["StreamingServiceId"] = new SelectList(_context.StreamingService, "Id", "Name", movieStreaming.StreamingServiceId);
            return View(movieStreaming);
        }

        // GET: MovieStreaming/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieStreaming == null)
            {
                return NotFound();
            }

            var movieStreaming = await _context.MovieStreaming
                .Include(m => m.Movie)
                .Include(m => m.StreamingService)
                .FirstOrDefaultAsync(m => m.MovieStreamingId == id);
            if (movieStreaming == null)
            {
                return NotFound();
            }

            return View(movieStreaming);
        }

        // POST: MovieStreaming/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieStreaming == null)
            {
                return Problem("Entity set 'MvcMovieContext.MovieStreaming'  is null.");
            }
            var movieStreaming = await _context.MovieStreaming.FindAsync(id);
            if (movieStreaming != null)
            {
                _context.MovieStreaming.Remove(movieStreaming);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieStreamingExists(int id)
        {
          return (_context.MovieStreaming?.Any(e => e.MovieStreamingId == id)).GetValueOrDefault();
        }
    }
}
