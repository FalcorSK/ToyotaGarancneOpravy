#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyotaGarancneOpravy.Models;

namespace ToyotaGarancneOpravy.Controllers
{
    public class PrilohyController : Controller
    {
        private readonly Toyota_DBContext _context;

        public PrilohyController(Toyota_DBContext context)
        {
            _context = context;
        }

        // GET: Prilohy
        public async Task<IActionResult> Index()
        {
            var toyota_DBContext = _context.prilohy.Include(p => p.garancnaOprava);
            return View(await toyota_DBContext.ToListAsync());
        }

        // GET: Prilohy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priloha = await _context.prilohy
                .Include(p => p.garancnaOprava)
                .FirstOrDefaultAsync(m => m.PrilohaId == id);
            if (priloha == null)
            {
                return NotFound();
            }

            return View(priloha);
        }

        // GET: Prilohy/Create
        public IActionResult Create()
        {
            ViewData["GarancnaOpravaId"] = new SelectList(_context.garancneOpravy, "GarancnaOpravaId", "GarancnaOpravaId");
            return View();
        }

        // POST: Prilohy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrilohaId,GarancnaOpravaId,PrilohaTyp,PrilohaNazov,PrilohaSubor,PrilohaFileName,CreateDate,CreateBy,ModifiedDate,ModifiiedBy")] Priloha priloha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priloha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GarancnaOpravaId"] = new SelectList(_context.garancneOpravy, "GarancnaOpravaId", "GarancnaOpravaId", priloha.GarancnaOpravaId);
            return View(priloha);
        }

        // GET: Prilohy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priloha = await _context.prilohy.FindAsync(id);
            if (priloha == null)
            {
                return NotFound();
            }
            ViewData["GarancnaOpravaId"] = new SelectList(_context.garancneOpravy, "GarancnaOpravaId", "GarancnaOpravaId", priloha.GarancnaOpravaId);
            return View(priloha);
        }

        // POST: Prilohy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrilohaId,GarancnaOpravaId,PrilohaTyp,PrilohaNazov,PrilohaSubor,PrilohaFileName,CreateDate,CreateBy,ModifiedDate,ModifiiedBy")] Priloha priloha)
        {
            if (id != priloha.PrilohaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priloha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrilohaExists(priloha.PrilohaId))
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
            ViewData["GarancnaOpravaId"] = new SelectList(_context.garancneOpravy, "GarancnaOpravaId", "GarancnaOpravaId", priloha.GarancnaOpravaId);
            return View(priloha);
        }

        // GET: Prilohy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priloha = await _context.prilohy
                .Include(p => p.garancnaOprava)
                .FirstOrDefaultAsync(m => m.PrilohaId == id);
            if (priloha == null)
            {
                return NotFound();
            }

            return View(priloha);
        }

        // POST: Prilohy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priloha = await _context.prilohy.FindAsync(id);
            _context.prilohy.Remove(priloha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrilohaExists(int id)
        {
            return _context.prilohy.Any(e => e.PrilohaId == id);
        }
    }
}
