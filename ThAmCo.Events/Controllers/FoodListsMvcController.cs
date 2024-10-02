using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Controllers
{
    public class FoodListsMvcController : Controller
    {
        private readonly CateringDbContext _context;

        public FoodListsMvcController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: FoodListsMvc
        public async Task<IActionResult> Index()
        {
            var cateringDbContext = _context.FoodList.Include(f => f.Menu);
            return View(await cateringDbContext.ToListAsync());
        }

        // GET: FoodListsMvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodList == null)
            {
                return NotFound();
            }

            var foodList = await _context.FoodList
                .Include(f => f.Menu)
                .FirstOrDefaultAsync(m => m.FoodListId == id);
            if (foodList == null)
            {
                return NotFound();
            }

            return View(foodList);
        }

        // GET: FoodListsMvc/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId");
            return View();
        }

        // POST: FoodListsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodListId,FoodName,MenuId,Price")] FoodList foodList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodList.MenuId);
            return View(foodList);
        }

        // GET: FoodListsMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodList == null)
            {
                return NotFound();
            }

            var foodList = await _context.FoodList.FindAsync(id);
            if (foodList == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodList.MenuId);
            return View(foodList);
        }

        // POST: FoodListsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodListId,FoodName,MenuId,Price")] FoodList foodList)
        {
            if (id != foodList.FoodListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodListExists(foodList.FoodListId))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "MenuId", "MenuId", foodList.MenuId);
            return View(foodList);
        }

        // GET: FoodListsMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodList == null)
            {
                return NotFound();
            }

            var foodList = await _context.FoodList
                .Include(f => f.Menu)
                .FirstOrDefaultAsync(m => m.FoodListId == id);
            if (foodList == null)
            {
                return NotFound();
            }

            return View(foodList);
        }

        // POST: FoodListsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodList == null)
            {
                return Problem("Entity set 'CateringDbContext.FoodList'  is null.");
            }
            var foodList = await _context.FoodList.FindAsync(id);
            if (foodList != null)
            {
                _context.FoodList.Remove(foodList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodListExists(int id)
        {
            return _context.FoodList.Any(e => e.FoodListId == id);
        }
    }
}
