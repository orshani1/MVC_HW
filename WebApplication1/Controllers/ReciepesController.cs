using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReciepesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReciepesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reciepes
        public async Task<IActionResult> Index()
        {
              return _context.Reciepes != null ? 
                          View(await _context.Reciepes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Reciepes'  is null.");
        }

        // GET: Reciepes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reciepes == null)
            {
                return NotFound();
            }

            var reciepe = await _context.Reciepes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciepe == null)
            {
                return NotFound();
            }

            return View(reciepe);
        }

        // GET: Reciepes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reciepes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CakeName,ReciepeLevels")] Reciepe reciepe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reciepe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reciepe);
        }

        // GET: Reciepes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reciepes == null)
            {
                return NotFound();
            }

            var reciepe = await _context.Reciepes.FindAsync(id);
            if (reciepe == null)
            {
                return NotFound();
            }
            return View(reciepe);
        }

        // POST: Reciepes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CakeName,ReciepeLevels")] Reciepe reciepe)
        {
            if (id != reciepe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reciepe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReciepeExists(reciepe.Id))
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
            return View(reciepe);
        }

        // GET: Reciepes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reciepes == null)
            {
                return NotFound();
            }

            var reciepe = await _context.Reciepes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciepe == null)
            {
                return NotFound();
            }

            return View(reciepe);
        }

        // POST: Reciepes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reciepes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reciepes'  is null.");
            }
            var reciepe = await _context.Reciepes.FindAsync(id);
            if (reciepe != null)
            {
                _context.Reciepes.Remove(reciepe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciepeExists(int id)
        {
          return (_context.Reciepes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
