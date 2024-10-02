using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingsApiController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodBookingsApiController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodBookingsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodBooking>>> GetFoodBooking()
        {
            return await _context.FoodBooking.ToListAsync();
        }

        // GET: api/FoodBookingsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodBooking>> GetFoodBooking(string id)
        {
            var foodBooking = await _context.FoodBooking.FindAsync(id);

            if (foodBooking == null)
            {
                return NotFound();
            }

            return foodBooking;
        }

        // PUT: api/FoodBookingsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodBooking(string id, FoodBooking foodBooking)
        {
            if (id != foodBooking.FoodBookingId)
            {
                return BadRequest();
            }

            _context.Entry(foodBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodBookingExists(id))
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

        // POST: api/FoodBookingsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodBooking>> PostFoodBooking(FoodBooking foodBooking)
        {
            _context.FoodBooking.Add(foodBooking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodBookingExists(foodBooking.FoodBookingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFoodBooking", new { id = foodBooking.FoodBookingId }, foodBooking);
        }

        // DELETE: api/FoodBookingsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodBooking(string id)
        {
            var foodBooking = await _context.FoodBooking.FindAsync(id);
            if (foodBooking == null)
            {
                return NotFound();
            }

            _context.FoodBooking.Remove(foodBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodBookingExists(string id)
        {
            return _context.FoodBooking.Any(e => e.FoodBookingId == id);
        }
    }
}
