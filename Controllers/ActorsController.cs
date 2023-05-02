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
    public class ActorsController : Controller
    {
        private readonly MvcMovieContext _context;

        public ActorsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return _context.Actor != null ? 
                          View(await _context.Actor.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.Actor'  is null.");
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (actor == null)
            {
                return NotFound();
            }
            
            var stars= _context.Star.Where(s => s.ActorId == actor.Id);
            var movies = _context.Movie;
            foreach(var star in stars)
            {
                foreach(var movie in movies)
                { 
                    if(movie.Id.Equals(star.MovieId)){  
                        actor.Stars.Add(star);
                    }
                }
            }            

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            ViewData["movies"] = _context.Movie;
            ViewData["stars"] = _context.Star;
            ViewBag.Stars = new MultiSelectList(_context.Movie, "Id", "Title");
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Dob")] Actor actor, List<Guid> Stars)
        {
            if (ModelState.IsValid)
            {   
                Guid g = Guid.NewGuid();
                actor.Id=g;
                _context.Add(actor);
                if(Stars!=null){
                    foreach (var item in Stars)
                    {
                        Star s = new Star();
                        s.MovieId=item;
                        s.ActorId=g;
                        _context.Add(s);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            var allStars=_context.Star.Where(s=> s.ActorId.Equals(id));
            List<Guid> movieIDs = new List<Guid>();

            foreach (var star in allStars)
            {
                if(star.MovieId!=null){
                    movieIDs.Add((Guid)star.MovieId);
                }
            }

            ViewBag.Stars = new MultiSelectList(_context.Movie, "Id", "Title",movieIDs);

            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Dob")] Actor actor, List<Guid> Stars)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    if(Stars!=null){
                    foreach (var item in Stars)
                        {
                            Star s = new Star();
                            s.MovieId=item;
                            s.ActorId=id;
                            _context.Add(s);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Actor == null)
            {
                return Problem("Entity set 'MvcMovieContext.Actor'  is null.");
            }
            var actor = await _context.Actor.FindAsync(id);
            if (actor != null)
            {
                var starMatches = _context.Star.Where(star => star.ActorId == id);
                foreach (var item in starMatches)
                {
                    _context.Star.Remove(item);
                }
                _context.Actor.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(Guid id)
        {
          return (_context.Actor?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
