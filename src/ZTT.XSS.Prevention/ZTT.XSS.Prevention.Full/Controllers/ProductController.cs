using System;
using System.Linq;
using System.Threading.Tasks;
using ASPSecurityKit;
using ASPSecurityKit.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZTT.XSS.Prevention.Full.DataModels;
using ZTT.XSS.Prevention.Full.Models;
using ZTT.XSS.Prevention.Full.Repositories;

namespace ZTT.XSS.Prevention.Full.Controllers
{
    public class ProductController : ServiceControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context, IUserService<Guid, Guid, DbUser> userService, INetSecuritySettings securitySettings,
	        ISecurityUtility securityUtility, ILogger logger, IAuthSessionProvider authSessionProvider, IUserPermitRepository permitRepository) : base(userService, securitySettings, securityUtility,
	        logger)
        {
            _context = context;
        }

        // GET: Products
        [PossessesPermissionCode]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        [AuthAction("Index")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbProduct = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            return View(dbProduct);
        }

        // GET: Products/Create
        [PossessesPermissionCode]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PossessesPermissionCode]
        public async Task<IActionResult> Create([Bind("Name,Description,Cost")] NewProduct newProduct)
        {
	        if (ModelState.IsValid)
	        {
		        var dbProduct = new DbProduct
		        {
			        Id = Guid.NewGuid(),
			        Name = newProduct.Name,
			        Description = newProduct.Description,
			        OwnerId = this.UserService.CurrentUserId,
                    Cost = newProduct.Cost
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

            var dbProduct = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            return View(dbProduct);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
