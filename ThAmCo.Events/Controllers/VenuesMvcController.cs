﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Controllers
{
    public class VenuesMvcController : Controller
    {
        private readonly VenuesDbContext _context;

        public VenuesMvcController(VenuesDbContext context)
        {
            _context = context;
        }

        // GET: VenuesMvc
        public async Task<IActionResult> Index()
        {
              return View(await _context.Venues.ToListAsync());
        }

        // GET: VenuesMvc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.Code == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: VenuesMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VenuesMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Description,Capacity")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: VenuesMvc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: VenuesMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name,Description,Capacity")] Venue venue)
        {
            if (id != venue.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.Code))
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
            return View(venue);
        }

        // GET: VenuesMvc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.Code == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: VenuesMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Venues == null)
            {
                return Problem("Entity set 'VenuesDbContext.Venues'  is null.");
            }
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(string id)
        {
          return _context.Venues.Any(e => e.Code == id);
        }
    }
}
