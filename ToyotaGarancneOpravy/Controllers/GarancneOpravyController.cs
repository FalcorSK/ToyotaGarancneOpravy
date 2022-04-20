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
    public class GarancneOpravyController : Controller
    {
        private readonly Toyota_DBContext _context;

        public GarancneOpravyController(Toyota_DBContext context)
        {
            _context = context;
        }

        // GET: GarancneOpravy
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.garancneOpravy.ToListAsync());
        //}
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.garancneOpravy.ToListAsync());

            ViewData["CurrentFilter"] = SearchString;
            var resturnValue = from b in _context.garancneOpravy
                               select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                resturnValue = resturnValue.Where(b => b.ZakazkaTb.Contains(SearchString) || b.ZakazkaTg.Contains(SearchString));
            }

            return View("Index", await resturnValue.ToListAsync());
        }



        // GET: GarancneOpravy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var garancnaOprava = await _context.garancneOpravy
            //    .FirstOrDefaultAsync(m => m.GarancnaOpravaId == id);
            var garancnaOprava = await _context.garancneOpravy
               .Include(p => p.Prilohy)
               .Where(g => g.GarancnaOpravaId == id).FirstOrDefaultAsync();


            if (garancnaOprava == null)
            {
                return NotFound();
            }

            
            return View(garancnaOprava);
        }

        // GET: GarancneOpravy/Create
        public IActionResult Create()
        {
            GarancnaOprava garancnaOprava = new GarancnaOprava();
            //garancnaOprava.Prilohy.Add(new Priloha() { PrilohaId = 1 });


            return View(garancnaOprava);
        }

        // POST: GarancneOpravy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GarancnaOpravaId,ZakazkaTb,TbScan,TbFileName,ZakazkaTg,TgScan,TgFileName,Vin,Cws,Protokol,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] GarancnaOprava garancnaOprava)
        public async Task<IActionResult> Create(GarancnaOprava garancnaOprava)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(garancnaOprava);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(garancnaOprava);

            if (ModelState.IsValid)
            {
                if (garancnaOprava.TBFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        garancnaOprava.TBFile.CopyTo(ms);
                        garancnaOprava.TbScan = ms.ToArray();

                        garancnaOprava.TbFileName = garancnaOprava.TBFile.FileName;
                    }
                }

                if (garancnaOprava.TGFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        garancnaOprava.TGFile.CopyTo(ms);
                        garancnaOprava.TgScan = ms.ToArray();

                        garancnaOprava.TgFileName = garancnaOprava.TGFile.FileName;
                    }
                }

                //vytvor povinne prilohy
                foreach(var item in _context.prilohaTypy)
                {
                    garancnaOprava.Prilohy.Add(new Priloha() { PrilohaNazov = item.PrilohaNazov, PrilohaTyp = item.PrilohaTypId });
                }


                _context.Add(garancnaOprava);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garancnaOprava);


        }

        // GET: GarancneOpravy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garancnaOprava = await _context.garancneOpravy.FindAsync(id);
            if (garancnaOprava == null)
            {
                return NotFound();
            }
            return View(garancnaOprava);
        }

        // POST: GarancneOpravy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GarancnaOpravaId,ZakazkaTb,TbScan,TbFileName,ZakazkaTg,TgScan,TgFileName,Vin,Cws,Protokol,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] GarancnaOprava garancnaOprava)
        {
            if (id != garancnaOprava.GarancnaOpravaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garancnaOprava);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarancnaOpravaExists(garancnaOprava.GarancnaOpravaId))
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
            return View(garancnaOprava);
        }

        // GET: GarancneOpravy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garancnaOprava = await _context.garancneOpravy
                .FirstOrDefaultAsync(m => m.GarancnaOpravaId == id);
            if (garancnaOprava == null)
            {
                return NotFound();
            }

            return View(garancnaOprava);
        }

        // POST: GarancneOpravy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garancnaOprava = await _context.garancneOpravy.FindAsync(id);
            _context.garancneOpravy.Remove(garancnaOprava);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarancnaOpravaExists(int id)
        {
            return _context.garancneOpravy.Any(e => e.GarancnaOpravaId == id);
        }


        public async Task<FileStreamResult> FileOpenTB(int id)
        {
            if (id == 0) { return null; }
            var garancnaOprava = await _context.garancneOpravy.FindAsync(id);

            Stream filecontent;
            string contentType = string.Empty;
            string fileName = string.Empty;

            contentType = GetMIMEType(garancnaOprava.TbFileName);
            fileName = garancnaOprava.TbFileName;


            using (MemoryStream ms = new MemoryStream())
            {
                filecontent = new MemoryStream(garancnaOprava.TbScan);
            }

            return File(filecontent, contentType);
        }

        public async Task<FileStreamResult> FileOpenTG(int id)
        {
            if (id == 0) { return null; }
            var garancnaOprava = await _context.garancneOpravy.FindAsync(id);

            Stream filecontent;
            string contentType = string.Empty;
            string fileName = string.Empty;

            contentType = GetMIMEType(garancnaOprava.TgFileName);
            fileName = garancnaOprava.TgFileName;


            using (MemoryStream ms = new MemoryStream())
            {
                filecontent = new MemoryStream(garancnaOprava.TgScan);
            }

            return File(filecontent, contentType);
        }

        private string GetMIMEType(string fileName)
        {
            var provider =
                new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePrilohy(int id, [Bind("PrilohaId,GarancnaOpravaId,PrilohaTyp,PrilohaNazov,PrilohaSubor,PrilohaFileName,CreateDate,CreateBy,ModifiedDate,ModifiiedBy")] Priloha priloha)
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


        private bool PrilohaExists(int id)
        {
            return _context.prilohy.Any(e => e.PrilohaId == id);
        }



    }
}
