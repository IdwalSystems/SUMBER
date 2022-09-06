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
    public class JSuGredGajiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JSuGredGajiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JSuGredGaji
        public async Task<IActionResult> Index()
        {
            return View(await _context.JSuGredGaji.ToListAsync());
        }

        // GET: JSuGredGaji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuGredGaji = await _context.JSuGredGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jSuGredGaji == null)
            {
                return NotFound();
            }

            return View(jSuGredGaji);
        }

        // GET: JSuGredGaji/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JSuGredGaji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kod,Perihal,FlHapus,TarHapus,SuPekerjaMasukId,UserId,TarMasuk,SuPekerjaKemaskiniId,UserIdKemaskini,TarKemaskini")] JSuGredGaji jSuGredGaji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jSuGredGaji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jSuGredGaji);
        }

        // GET: JSuGredGaji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuGredGaji = await _context.JSuGredGaji.FindAsync(id);
            if (jSuGredGaji == null)
            {
                return NotFound();
            }
            return View(jSuGredGaji);
        }

        // POST: JSuGredGaji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kod,Perihal,FlHapus,TarHapus,SuPekerjaMasukId,UserId,TarMasuk,SuPekerjaKemaskiniId,UserIdKemaskini,TarKemaskini")] JSuGredGaji jSuGredGaji)
        {
            if (id != jSuGredGaji.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jSuGredGaji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSuGredGajiExists(jSuGredGaji.Id))
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
            return View(jSuGredGaji);
        }

        // GET: JSuGredGaji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuGredGaji = await _context.JSuGredGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jSuGredGaji == null)
            {
                return NotFound();
            }

            return View(jSuGredGaji);
        }

        // POST: JSuGredGaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jSuGredGaji = await _context.JSuGredGaji.FindAsync(id);
            _context.JSuGredGaji.Remove(jSuGredGaji);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JSuGredGajiExists(int id)
        {
            return _context.JSuGredGaji.Any(e => e.Id == id);
        }
    }
}
