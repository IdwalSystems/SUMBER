using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Sumber;

namespace SUMBER.Controllers
{
    public class JSuTarafJawatanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JSuTarafJawatanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JSuTarafJawatan
        public async Task<IActionResult> Index()
        {
            return View(await _context.JSuTarafJawatan.ToListAsync());
        }

        // GET: JSuTarafJawatan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuTarafJawatan = await _context.JSuTarafJawatan
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jSuTarafJawatan == null)
            {
                return NotFound();
            }

            return View(jSuTarafJawatan);
        }

        // GET: JSuTarafJawatan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JSuTarafJawatan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Kod,Perihal,FlHapus,TarHapus,SuPekerjaMasukId,UserId,TarMasuk,SuPekerjaKemaskiniId,UserIdKemaskini,TarKemaskini")] JSuTarafJawatan jSuTarafJawatan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jSuTarafJawatan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jSuTarafJawatan);
        }

        // GET: JSuTarafJawatan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuTarafJawatan = await _context.JSuTarafJawatan.FindAsync(id);
            if (jSuTarafJawatan == null)
            {
                return NotFound();
            }
            return View(jSuTarafJawatan);
        }

        // POST: JSuTarafJawatan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Kod,Perihal,FlHapus,TarHapus,SuPekerjaMasukId,UserId,TarMasuk,SuPekerjaKemaskiniId,UserIdKemaskini,TarKemaskini")] JSuTarafJawatan jSuTarafJawatan)
        {
            if (id != jSuTarafJawatan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jSuTarafJawatan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSuTarafJawatanExists(jSuTarafJawatan.ID))
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
            return View(jSuTarafJawatan);
        }

        // GET: JSuTarafJawatan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuTarafJawatan = await _context.JSuTarafJawatan
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jSuTarafJawatan == null)
            {
                return NotFound();
            }

            return View(jSuTarafJawatan);
        }

        // POST: JSuTarafJawatan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jSuTarafJawatan = await _context.JSuTarafJawatan.FindAsync(id);
            _context.JSuTarafJawatan.Remove(jSuTarafJawatan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JSuTarafJawatanExists(int id)
        {
            return _context.JSuTarafJawatan.Any(e => e.ID == id);
        }
    }
}
