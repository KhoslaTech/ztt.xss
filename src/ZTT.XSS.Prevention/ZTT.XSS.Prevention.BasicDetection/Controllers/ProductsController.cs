using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTT.XSS.Prevention.BasicDetection.Data;
using ZTT.XSS.Prevention.BasicDetection.Models;

namespace ZTT.XSS.Prevention.BasicDetection.Controllers
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
        public async Task<IActionResult> Create([Bind("Name,Description,Cost")] NewProduct newProduct)
        {
            if (ModelState.IsValid)
            {
	            var dbProduct = new DbProduct
	            {
		            Id = Guid.NewGuid(),
                    Name = newProduct.Name,
                    Description = newProduct.Description
	            };

	            _context.Add(dbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(newProduct);
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
