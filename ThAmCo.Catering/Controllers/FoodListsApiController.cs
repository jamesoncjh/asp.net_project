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
    public class FoodListsApiController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodListsApiController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodListsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodList>>> GetFoodList()
        {
            return await _context.FoodList.ToListAsync();
        }

        // GET: api/FoodListsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodList>> GetFoodList(int id)
        {
            var foodList = await _context.FoodList.FindAsync(id);

            if (foodList == null)
            {
                return NotFound();
            }

            return foodList;
        }

        // PUT: api/FoodListsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodList(int id, FoodList foodList)
        {
            if (id != foodList.FoodListId)
            {
                return BadRequest();
            }

            _context.Entry(foodList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodListExists(id))
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

        // POST: api/FoodListsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodList>> PostFoodList(FoodList foodList)
        {
            _context.FoodList.Add(foodList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodList", new { id = foodList.FoodListId }, foodList);
        }

        // DELETE: api/FoodListsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodList(int id)
        {
            var foodList = await _context.FoodList.FindAsync(id);
            if (foodList == null)
            {
                return NotFound();
            }

            _context.FoodList.Remove(foodList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodListExists(int id)
        {
            return _context.FoodList.Any(e => e.FoodListId == id);
        }
    }
}
