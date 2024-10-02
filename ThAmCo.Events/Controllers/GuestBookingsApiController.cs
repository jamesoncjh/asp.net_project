using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestBookingsAPIController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public GuestBookingsAPIController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: api/GuestBookingsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestBooking>>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        // GET: api/GuestBookingsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestBooking>> GetGuestBooking(int customerId, int eventId)
        {
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);

            if (guestBooking == null)
            {
                return NotFound();
            }

            return guestBooking;
        }

        // PUT: api/GuestBookingsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestBooking(int customerId,int eventId, GuestBooking guestBooking)
        {
            if (customerId != guestBooking.CustomerId && eventId !=guestBooking.EventId)
            {
                return BadRequest();
            }

            _context.Entry(guestBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestBookingExists(customerId,eventId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GuestBookingsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GuestBooking>> PostGuestBooking(int customerId, int eventId, GuestBooking guestBooking)
        {
            _context.Guests.Add(guestBooking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GuestBookingExists(customerId,eventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGuestBooking", new {customerId = guestBooking.CustomerId,eventId=guestBooking.EventId}, guestBooking);
        }

        // DELETE: api/GuestBookingsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestBooking(int customerId, int eventId)
        {
            var guestBooking = await _context.Guests.FindAsync(customerId, eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }

            _context.Guests.Remove(guestBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuestBookingExists(int customerId, int eventId)
        {
            return _context.Guests.Any(e => e.CustomerId == customerId && e.EventId==eventId);
        }
    }
}
