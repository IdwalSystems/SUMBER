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
        public const string modul = "JD015";
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

        // GET: JSuGredGaji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gredgaji = await _context.JSuGredGaji.FindAsync(id);
            if (gredgaji == null)
            {
                return NotFound();
            }
            return View(gredgaji);
        }

        // POST: JSuGredGaji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kod,Perihal")] JSuGredGaji gredgaji)
        {
            if (id != gredgaji.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.JSuGredGaji.FirstOrDefaultAsync(x => x.Id == gredgaji.Id);
                    var kodAsal = objAsal.Kod;
                    var perihalAsal = objAsal.Perihal;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    gredgaji.UserId = objAsal.UserId;
                    gredgaji.TarMasuk = objAsal.TarMasuk;
                    gredgaji.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    gredgaji.UserIdKemaskini = user.UserName;
                    gredgaji.TarKemaskini = DateTime.Now;
                    gredgaji.SuPekerjaKemaskiniId = pekerjaId;

                    _context.Update(gredgaji);

                    await AddLogAsync("Ubah", kodAsal + " -> " + gredgaji.Kod + ", "
                       + perihalAsal + " -> " + gredgaji.Perihal + ", ", gredgaji.Kod, id, 0, pekerjaId);

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSuGredGajiExists(gredgaji.Id))
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
            return View(gredgaji);
        }

        // GET: JSuGredGaji/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: JSuGredGaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var gredgaji = await _context.JSuGredGaji.FindAsync(id);

            gredgaji.UserIdKemaskini = user.UserName;
            gredgaji.TarKemaskini = DateTime.Now;
            gredgaji.SuPekerjaKemaskiniId = pekerjaId;

            _context.JSuGredGaji.Remove(gredgaji);
            await AddLogAsync("Hapus", gredgaji.Kod + " - " + gredgaji.Perihal, gredgaji.Kod, id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.JSuGredGaji.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.JSuGredGaji.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.Kod + " - " + obj.Perihal, obj.Kod, id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }

        private bool JSuGredGajiExists(int id)
        {
            return _context.JSuGredGaji.Any(e => e.Id == id);
        }

        private bool KodGredGajiExists(string kod)
        {
            return _context.JSuGredGaji.Any(e => e.Kod == kod);
        }
    }
}
