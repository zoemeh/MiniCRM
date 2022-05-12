using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniCRM.Data;
using MiniCRM.Models;

namespace MiniCRM.Controllers
{
    public class ClientAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientAddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientAddresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientAddresses.Include(c => c.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientAddresses/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientAddresses == null)
            {
                return NotFound();
            }

            var clientAddress = await _context.ClientAddresses
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientAddress == null)
            {
                return NotFound();
            }

            return View(clientAddress);
        }

        // GET: ClientAddresses/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            return View();
        }

        // POST: ClientAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,City,State,ZipCode,Country,AddressLine1,AddressLine2")] ClientAddress clientAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", clientAddress.ClientId);
            return View(clientAddress);
        }

        // GET: ClientAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientAddresses == null)
            {
                return NotFound();
            }

            var clientAddress = await _context.ClientAddresses.FindAsync(id);
            if (clientAddress == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", clientAddress.ClientId);
            return View(clientAddress);
        }

        // POST: ClientAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,City,State,ZipCode,Country,AddressLine1,AddressLine2")] ClientAddress clientAddress)
        {
            if (id != clientAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientAddressExists(clientAddress.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", clientAddress.ClientId);
            return View(clientAddress);
        }

        // GET: ClientAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientAddresses == null)
            {
                return NotFound();
            }

            var clientAddress = await _context.ClientAddresses
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientAddress == null)
            {
                return NotFound();
            }

            return View(clientAddress);
        }

        // POST: ClientAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientAddresses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClientAddresses'  is null.");
            }
            var clientAddress = await _context.ClientAddresses.FindAsync(id);
            if (clientAddress != null)
            {
                _context.ClientAddresses.Remove(clientAddress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientAddressExists(int id)
        {
          return (_context.ClientAddresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
