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
    public class StaffingsMvcController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffingsMvcController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: StaffingsMvc
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.Staffing.Include(s => s.Staff).Include(s => s.Event);
            return View(await eventsDbContext.ToListAsync());
        }

        // GET: StaffingsMvc/Details/5
        public async Task<IActionResult> Details(int? staffId, int? eventId)
        {
            if (staffId==null || eventId == null || _context.Staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.Staffing
                .Include(s => s.Staff)  
                .Include(s => s.Event)
                .FirstOrDefaultAsync(m => m.StaffId==staffId && m.EventId==eventId);
            if (staffing == null)
            {
                return NotFound();
            }

            return View(staffing);
        }

        // GET: StaffingsMvc/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Email");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title");
            return View();
        }

        // POST: StaffingsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,EventId,Attended")] Staffing staffing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Email",staffing.StaffId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffing.EventId);
            return View(staffing);
        }

        // GET: StaffingsMvc/Edit/5
        public async Task<IActionResult> Edit(int? staffId, int? eventId)
        {
            if (staffId == null || eventId ==null || _context.Staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.Staffing.FindAsync(staffId, eventId);
            if (staffing == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Email", staffing.StaffId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffing.EventId);
            return View(staffing);
        }

        // POST: StaffingsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,EventId,Attended")] Staffing staffing)
        {
            if (id != staffing.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffingExists(staffing.StaffId,staffing.EventId))
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
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Email", staffing.StaffId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Title", staffing.EventId);
            return View(staffing);
        }

        // GET: StaffingsMvc/Delete/5
        public async Task<IActionResult> Delete(int? staffId, int? eventId)
        {
            if (staffId== null || eventId==null||_context.Staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.Staffing
                .Include(s => s.Staff)
                .Include(s => s.Event)
                .FirstOrDefaultAsync(m => m.StaffId == staffId && m.EventId==eventId);
            if (staffing == null)
            {
                return NotFound();
            }

            return View(staffing);
        }

        // POST: StaffingsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int staffId, int eventId)
        {
            if (_context.Staffing == null)
            {
                return Problem("Entity set 'EventsDbContext.Staffing'  is null.");
            }
            var staffing = await _context.Staffing.FindAsync(staffId, eventId);
            if (staffing != null)
            {
                _context.Staffing.Remove(staffing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffingExists(int staffId, int eventId)
        {
          return _context.Staffing.Any(e => e.StaffId == staffId && e.EventId == eventId);
        }
    }
}
