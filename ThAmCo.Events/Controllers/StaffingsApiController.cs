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
    public class StaffingsApiController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public StaffingsApiController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: api/StaffingsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staffing>>> GetStaffing()
        {
            return await _context.Staffing.ToListAsync();
        }

        // GET: api/StaffingsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staffing>> GetStaffing(int staffId, int eventId)
        {
            var staffing = await _context.Staffing.FindAsync(staffId, eventId);

            if (staffing == null)
            {
                return NotFound();
            }

            return staffing;
        }

        // PUT: api/StaffingsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffing(int staffId, int eventId, Staffing staffing)
        {
            if (staffId != staffing.StaffId && eventId != staffing.EventId)
            {
                return BadRequest();
            }

            _context.Entry(staffing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffingExists(staffId, eventId))
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

        // POST: api/StaffingsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staffing>> PostStaffing(int staffId, int eventId, Staffing staffing)
        {
            _context.Staffing.Add(staffing);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StaffingExists(staffId, eventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStaffing", new { staffId = staffing.StaffId, eventId =staffing.EventId }, staffing);
        }

        // DELETE: api/StaffingsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffing(int staffId, int eventId)
        {
            var staffing = await _context.Staffing.FindAsync(staffId, eventId);
            if (staffing == null)
            {
                return NotFound();
            }

            _context.Staffing.Remove(staffing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffingExists(int staffId, int eventId)
        {
            return _context.Staffing.Any(e => e.StaffId == staffId&&e.EventId== eventId);
        }
    }
}
