using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTT.XSS.Attack.Data;

namespace ZTT.XSS.Attack.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ClassifiedsDbContext _context;

        public ProductsController(ClassifiedsDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.DbProduct.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbProduct = await _context.DbProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            return View(dbProduct);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Cost")] DbProduct dbProduct)
        {
            if (ModelState.IsValid)
            {
                dbProduct.Id = Guid.NewGuid();
                _context.Add(dbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbProduct);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbProduct = await _context.DbProduct.FindAsync(id);
            if (dbProduct == null)
            {
                return NotFound();
            }
            return View(dbProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Cost")] DbProduct dbProduct)
        {
            if (id != dbProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbProductExists(dbProduct.Id))
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
            return View(dbProduct);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbProduct = await _context.DbProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            return View(dbProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dbProduct = await _context.DbProduct.FindAsync(id);
            _context.DbProduct.Remove(dbProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbProductExists(Guid id)
        {
            return _context.DbProduct.Any(e => e.Id == id);
        }
    }
}
