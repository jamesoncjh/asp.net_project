using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Controllers
{
    public class GuestBookingsMvcController : Controller
    {
        private readonly EventsDbContext _context;

        public GuestBookingsMvcController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: GuestBookingsMvc
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.Guests.Include(g => g.Customer).Include(g => g.Event);
            return View(await eventsDbContext.ToListAsync());
        }

        // GET: GuestBookingsMvc/Details/5
        public async Task<IActionResult> Details(int? customerId, int? eventId)
        {
            if (customerId == null ||eventId==null || _context.Guests == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.Guests
                .Include(g => g.Customer)
                .Include(g => g.Event)
                .FirstOrDefaultAsync(m => m.CustomerId == customerId && m.EventId ==eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }

            return View(guestBooking);
        }

        // GET: GuestBookingsMvc/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            return View();
        }

        // POST: GuestBookingsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
            return View(guestBooking);
        }

        // GET: GuestBookingsMvc/Edit/5
        public async Task<IActionResult> Edit(int? customerId, int? eventId)
        {
            if (customerId == null || eventId == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
            return View(guestBooking);
        }

        // POST: GuestBookingsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int customerId, int eventId, bool attended, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {
            if ((customerId != guestBooking.CustomerId) && (eventId != guestBooking.CustomerId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookingExists(guestBooking.CustomerId,guestBooking.EventId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", guestBooking.CustomerId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", guestBooking.EventId);
            return View(guestBooking);
        }

        // GET: GuestBookingsMvc/Delete/5
        public async Task<IActionResult> Delete(int? customerId, int? eventId)
        {
            if (customerId == null || eventId==null|| _context.Guests == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.Guests
                .Include(g => g.Customer)
                .Include(g => g.Event)
                .FirstOrDefaultAsync(m => m.CustomerId == customerId && m.EventId == eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }

            return View(guestBooking);
        }

        // POST: GuestBookingsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int customerId, int eventId)
        {
            if (_context.Guests == null)
            {
                return Problem("Entity set 'EventsDbContext.Guests'  is null.");
            }
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            if (guestBooking != null)
            {
                _context.Guests.Remove(guestBooking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookingExists(int customerId, int eventId)
        {
          return _context.Guests.Any(e => e.CustomerId == customerId && e.EventId==eventId);
        }
    }
}
