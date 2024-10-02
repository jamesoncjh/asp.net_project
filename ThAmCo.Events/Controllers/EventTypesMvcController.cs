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
    public class EventTypesMvcController : Controller
    {
        private readonly VenuesDbContext _context;

        public EventTypesMvcController(VenuesDbContext context)
        {
            _context = context;
        }

        // GET: EventTypesMvc
        public async Task<IActionResult> Index()
        {
              return View(await _context.EventTypes.ToListAsync());
        }

        // GET: EventTypesMvc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EventTypes == null)
            {
                return NotFound();
            }

            var eventType = await _context.EventTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }

            return View(eventType);
        }

        // GET: EventTypesMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventTypesMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] EventType eventType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventType);
        }

        // GET: EventTypesMvc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EventTypes == null)
            {
                return NotFound();
            }

            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }
            return View(eventType);
        }

        // POST: EventTypesMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title")] EventType eventType)
        {
            if (id != eventType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTypeExists(eventType.Id))
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
            return View(eventType);
        }

        // GET: EventTypesMvc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EventTypes == null)
            {
                return NotFound();
            }

            var eventType = await _context.EventTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }

            return View(eventType);
        }

        // POST: EventTypesMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EventTypes == null)
            {
                return Problem("Entity set 'VenuesDbContext.EventTypes'  is null.");
            }
            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType != null)
            {
                _context.EventTypes.Remove(eventType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventTypeExists(string id)
        {
          return _context.EventTypes.Any(e => e.Id == id);
        }
    }
}
