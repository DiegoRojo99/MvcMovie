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
    public class StarsController : Controller
    {
        private readonly MvcMovieContext _context;

        public StarsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Stars
        public async Task<IActionResult> Index()
        {
              return _context.Star != null ? 
                          View(await _context.Star.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.Star'  is null.");
        }

        // GET: Stars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Star == null)
            {
                return NotFound();
            }

            var star = await _context.Star
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (star == null)
            {
                return NotFound();
            }

            var movies = _context.Movie;
            var actors = _context.Actor;

            return View(star);
        }

        // GET: Stars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarId,MovieId,ActorId")] Star star)
        {
            if (ModelState.IsValid)
            {
                _context.Add(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(star);
        }

        // GET: Stars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Star == null)
            {
                return NotFound();
            }

            var star = await _context.Star.FindAsync(id);
            if (star == null)
            {
                return NotFound();
            }
            return View(star);
        }

        // POST: Stars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarId,MovieId,ActorId")] Star star)
        {
            if (id != star.StarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(star);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarExists(star.StarId))
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
            return View(star);
        }

        // GET: Stars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Star == null)
            {
                return NotFound();
            }

            var star = await _context.Star
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // POST: Stars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Star == null)
            {
                return Problem("Entity set 'MvcMovieContext.Star'  is null.");
            }
            var star = await _context.Star.FindAsync(id);
            if (star != null)
            {
                _context.Star.Remove(star);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarExists(int id)
        {
          return (_context.Star?.Any(e => e.StarId == id)).GetValueOrDefault();
        }
    }
}
