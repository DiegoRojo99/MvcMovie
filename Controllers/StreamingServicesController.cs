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
    public class StreamingServicesController : Controller
    {
        private readonly MvcMovieContext _context;

        public StreamingServicesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: StreamingServices
        public async Task<IActionResult> Index()
        {
              return _context.StreamingService != null ? 
                          View(await _context.StreamingService.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.StreamingService'  is null.");
        }

        // GET: StreamingServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.StreamingService == null)
            {
                return NotFound();
            }

            var streamingService = await _context.StreamingService
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streamingService == null)
            {
                return NotFound();
            }

            return View(streamingService);
        }

        // GET: StreamingServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StreamingServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LogoImage")] StreamingService streamingService)
        {
            if (ModelState.IsValid)
            {
                streamingService.Id = Guid.NewGuid();
                _context.Add(streamingService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(streamingService);
        }

        // GET: StreamingServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.StreamingService == null)
            {
                return NotFound();
            }

            var streamingService = await _context.StreamingService.FindAsync(id);
            if (streamingService == null)
            {
                return NotFound();
            }
            return View(streamingService);
        }

        // POST: StreamingServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LogoImage")] StreamingService streamingService)
        {
            if (id != streamingService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streamingService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreamingServiceExists(streamingService.Id))
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
            return View(streamingService);
        }

        // GET: StreamingServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.StreamingService == null)
            {
                return NotFound();
            }

            var streamingService = await _context.StreamingService
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streamingService == null)
            {
                return NotFound();
            }

            return View(streamingService);
        }

        // POST: StreamingServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.StreamingService == null)
            {
                return Problem("Entity set 'MvcMovieContext.StreamingService'  is null.");
            }
            var streamingService = await _context.StreamingService.FindAsync(id);
            if (streamingService != null)
            {
                _context.StreamingService.Remove(streamingService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreamingServiceExists(Guid id)
        {
          return (_context.StreamingService?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
