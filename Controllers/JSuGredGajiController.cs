using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using SUMBER.Models.Modules;
using SUMBER.Models.Sumber;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SUMBER.Controllers
{

    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class JSuGredGajiController : Controller
    {
        public const string modul = "JD014";
        public const string namamodul = "Jadual Gred Gaji";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;

        public JSuGredGajiController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            AppLogIRepository<AppLog, int> appLog)
        {
            _context = context;
            _userManager = userManager;
            _appLog = appLog;
        }

        // GET: JSuGredGaji

        private async Task AddLogAsync(
            string operasi,
            string nota,
            string rujukan,
            int idRujukan,
            decimal jumlah,
            int? pekerjaId)
        {
            var user = await _userManager.GetUserAsync(User);
            AppLog appLog = new AppLog();

            appLog.IdRujukan = idRujukan;
            appLog.UserId = user.UserName;
            appLog.NoRujukan = rujukan;
            appLog.LgNote = namamodul + " - " + nota;
            appLog.Jumlah = jumlah;
            appLog.SuPekerjaId = pekerjaId;

            await _appLog.Insert(appLog, modul, operasi);
        }

        public async Task<IActionResult> Index()
        {
            var obj = await _context.JSuGredGaji.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.JSuGredGaji.IgnoreQueryFilters().ToListAsync();
            }

            return View(obj);
        }

        // GET: JSuGredGaji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gredgaji = await _context.JSuGredGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gredgaji == null)
            {
                return NotFound();
            }

            return View(gredgaji);
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

        public async Task<IActionResult> Create([Bind("Id,Kod,Perihal")] JSuGredGaji gredgaji)
        {
            if (KodGredGajiExists(gredgaji.Kod) == false)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    gredgaji.UserId = user.UserName;
                    gredgaji.TarMasuk = DateTime.Now;
                    gredgaji.SuPekerjaMasukId = pekerjaId;

                    _context.Add(gredgaji);
                    await AddLogAsync("Tambah", gredgaji.Kod + " - " + gredgaji.Perihal, gredgaji.Kod, 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

                }
            }
            else
            {
                TempData[SD.Error] = "Kod ini telah wujud..!";
            }
            return View(gredgaji);
        }

        private bool KodGredGajiExists(string kod)
        {
            return _context.JSuGredGaji.Any(e => e.Kod == kod);
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
