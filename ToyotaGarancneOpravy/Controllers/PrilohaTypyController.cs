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
    public class PrilohaTypyController : Controller
    {
        private readonly Toyota_DBContext _context;

        public PrilohaTypyController(Toyota_DBContext context)
        {
            _context = context;
        }

        // GET: PrilohaTypy
        public async Task<IActionResult> Index()
        {
            return View(await _context.prilohaTypy.ToListAsync());
        }

        // GET: PrilohaTypy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prilohaTyp = await _context.prilohaTypy
                .FirstOrDefaultAsync(m => m.PrilohaTypId == id);
            if (prilohaTyp == null)
            {
                return NotFound();
            }

            return View(prilohaTyp);
        }

        // GET: PrilohaTypy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrilohaTypy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrilohaTypId,PrilohaNazov,PrilohaPopis,Aktivna,CreatedDate,CreateddBy,ModifiedDate,ModifiedBy")] PrilohaTyp prilohaTyp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prilohaTyp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prilohaTyp);
        }

        // GET: PrilohaTypy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prilohaTyp = await _context.prilohaTypy.FindAsync(id);
            if (prilohaTyp == null)
            {
                return NotFound();
            }
            return View(prilohaTyp);
        }

        // POST: PrilohaTypy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrilohaTypId,PrilohaNazov,PrilohaPopis,Aktivna,CreatedDate,CreateddBy,ModifiedDate,ModifiedBy")] PrilohaTyp prilohaTyp)
        {
            if (id != prilohaTyp.PrilohaTypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prilohaTyp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrilohaTypExists(prilohaTyp.PrilohaTypId))
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
            return View(prilohaTyp);
        }

        // GET: PrilohaTypy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prilohaTyp = await _context.prilohaTypy
                .FirstOrDefaultAsync(m => m.PrilohaTypId == id);
            if (prilohaTyp == null)
            {
                return NotFound();
            }

            return View(prilohaTyp);
        }

        // POST: PrilohaTypy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prilohaTyp = await _context.prilohaTypy.FindAsync(id);
            _context.prilohaTypy.Remove(prilohaTyp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrilohaTypExists(int id)
        {
            return _context.prilohaTypy.Any(e => e.PrilohaTypId == id);
        }
    }
}
