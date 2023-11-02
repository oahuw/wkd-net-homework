using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cart.Data;
using Cart.Models;

namespace Cart.Controllers
{
    public class CartsController : Controller
    {
        private readonly CartContext _context;

        public CartsController(CartContext context)
        {
            _context = context;
        }

        // GET: Carts
        /*public async Task<IActionResult> Index()
        {
              return _context.Carts != null ? 
                          View(await _context.Carts.ToListAsync()) :
                          Problem("Entity set 'CartContext.Carts'  is null.");
        }*/
        public async Task<IActionResult> Index(decimal? low1, decimal? high1,decimal? low2,decimal? high2)
        {
            if(low1 != null || high1 != null && (low2 != null || high2 != 2))
            {
                 var carts = from m in _context.Carts
                                         where   low1 <= m.ProductPrice
                                         && m.ProductPrice <= high1 
                                         && low2 <= m.ProductPrice
                                         && m.ProductPrice <= high2
                             select m;
                return View(await carts.ToListAsync());
            }
            else if (low1 != null || high1 != null)
            {
                var carts = from m in _context.Carts
                            where low2 <= m.ProductPrice
                            && m.ProductPrice <= high2
                            select m;
                return View(await carts.ToListAsync());
            }
            else  if(low2 != null || high2 != null)
            {
                var  carts = from m in _context.Carts
                             where low2 <= m.ProductPrice
                             && m.ProductPrice <= high2
                             select m;
                return View(await carts.ToListAsync());
            }
            return _context.Carts != null ?
                          View(await _context.Carts.ToListAsync()) :
                          Problem("Entity set 'CartContext.Carts'  is null.");


        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CartId,ProductId,ProductPrice,userId,CreatedDate")] Carts carts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carts);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts.FindAsync(id);
            if (carts == null)
            {
                return NotFound();
            }
            return View(carts);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CartId,ProductId,ProductPrice,userId,CreatedDate")] Carts carts)
        {
            if (id != carts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartsExists(carts.Id))
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
            return View(carts);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'CartContext.Carts'  is null.");
            }
            var carts = await _context.Carts.FindAsync(id);
            if (carts != null)
            {
                _context.Carts.Remove(carts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartsExists(int id)
        {
          return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
