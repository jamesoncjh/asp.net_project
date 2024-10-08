﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Controllers
{
    public class FoodBookingsMvcController : Controller
    {
        private readonly CateringDbContext _context;

        public FoodBookingsMvcController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: FoodBookingsMvc
        public async Task<IActionResult> Index()
        {
            var cateringDbContext = _context.FoodBooking.Include(f => f.Menu);
            return View(await cateringDbContext.ToListAsync());
        }

        // GET: FoodBookingsMvc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FoodBooking == null)
            {
                return NotFound();
            }

            var foodBooking = await _context.FoodBooking
                .Include(f => f.Menu)
                .FirstOrDefaultAsync(m => m.FoodBookingId == id);
            if (foodBooking == null)
            {
                return NotFound();
            }

            return View(foodBooking);
        }

        // GET: FoodBookingsMvc/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId");
            return View();
        }

        // POST: FoodBookingsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodBookingId,Date,MenuId")] FoodBooking foodBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodBooking.MenuId);
            return View(foodBooking);
        }

        // GET: FoodBookingsMvc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FoodBooking == null)
            {
                return NotFound();
            }

            var foodBooking = await _context.FoodBooking.FindAsync(id);
            if (foodBooking == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodBooking.MenuId);
            return View(foodBooking);
        }

        // POST: FoodBookingsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FoodBookingId,Date,MenuId")] FoodBooking foodBooking)
        {
            if (id != foodBooking.FoodBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodBookingExists(foodBooking.FoodBookingId))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodBooking.MenuId);
            return View(foodBooking);
        }

        // GET: FoodBookingsMvc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FoodBooking == null)
            {
                return NotFound();
            }

            var foodBooking = await _context.FoodBooking
                .Include(f => f.Menu)
                .FirstOrDefaultAsync(m => m.FoodBookingId == id);
            if (foodBooking == null)
            {
                return NotFound();
            }

            return View(foodBooking);
        }

        // POST: FoodBookingsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FoodBooking == null)
            {
                return Problem("Entity set 'CateringDbContext.FoodBooking'  is null.");
            }
            var foodBooking = await _context.FoodBooking.FindAsync(id);
            if (foodBooking != null)
            {
                _context.FoodBooking.Remove(foodBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodBookingExists(string id)
        {
            return _context.FoodBooking.Any(e => e.FoodBookingId == id);
        }
    }
}
