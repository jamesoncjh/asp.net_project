using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;

namespace ThAmCo.Events.Controllers
{
    public class ReservationsMvcController : Controller
    {
        private readonly VenuesDbContext _context;

        public ReservationsMvcController(VenuesDbContext context)
        {
            _context = context;
        }

        // GET: ReservationsMvc
        public async Task<IActionResult> Index()
        {
            var venuesDbContext = _context.Reservations.Include(r => r.Availability);
            return View(await venuesDbContext.ToListAsync());
        }

        // GET: ReservationsMvc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Availability)
                .FirstOrDefaultAsync(m => m.Reference == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: ReservationsMvc/Create
        public IActionResult Create()
        {
            ViewData["EventDate"] = new SelectList(_context.Availabilities, "", "Date");
            ViewData["VenueCode"] = new SelectList(_context.Availabilities, "", "VenueCode", " ", "Date");
            return View();
        }

        // POST: ReservationsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reference,EventDate,VenueCode,WhenMade,StaffId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventDate"] = new SelectList(_context.Availabilities, "", "Date");
            ViewData["VenueCode"] = new SelectList(_context.Availabilities, "", "VenueCode", " ", "Date");
            return View(reservation);
        }

        // GET: ReservationsMvc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["EventDate"] = new SelectList(_context.Availabilities, "", "Date");
            ViewData["VenueCode"] = new SelectList(_context.Availabilities, "", "VenueCode", " ","Date");
            return View(reservation);
        }

        // POST: ReservationsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Reference,EventDate,VenueCode,WhenMade,StaffId")] Reservation reservation)
        {
            if (id != reservation.Reference)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Reference))
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
            ViewData["EventDate"] = new SelectList(_context.Availabilities, "", "Date");
            ViewData["VenueCode"] = new SelectList(_context.Availabilities, "", "VenueCode", " ", "Date");
            return View(reservation);
        }

        // GET: ReservationsMvc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Availability)
                .FirstOrDefaultAsync(m => m.Reference == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: ReservationsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'VenuesDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(string id)
        {
          return _context.Reservations.Any(e => e.Reference == id);
        }
    }
}
