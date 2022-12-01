using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules;
using SUMBER.Models.Modules.IRepository;
using SUMBER.Models.Sumber;
using System.Data;

namespace SUMBER.Controllers
{
    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class JSuKodGajiController : Controller
    {
        public const string modul = "JD014";
        public const string namamodul = "Jadual Kod Gaji";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;

        public JSuKodGajiController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            AppLogIRepository<AppLog, int> appLog)
        {
            _context = context;
            _userManager = userManager;
            _appLog = appLog;
        }

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

        // GET: JSuKodGaji
        public async Task<IActionResult> Index()
        {
            var obj = await _context.JSuKodGaji.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.JSuKodGaji.IgnoreQueryFilters().ToListAsync();
            }

            return View(obj);
        }

        // GET: JSuKodGaji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KodGaji = await _context.JSuKodGaji
                .FirstOrDefaultAsync(m => m.ID == id);
            if (KodGaji == null)
            {
                return NotFound();
            }

            return View(KodGaji);
        }

        // GET: JSuKodGaji/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JSuKodGaji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kod,Perihal")] JSuKodGaji kodGaji, int FlJenis)
        {
            if (KodGajiExists(kodGaji.Kod) == false)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    kodGaji.UserId = user.UserName;
                    kodGaji.TarMasuk = DateTime.Now;
                    kodGaji.SuPekerjaMasukId = pekerjaId;
                    kodGaji.FlJenis = FlJenis;

                    _context.Add(kodGaji);
                    await AddLogAsync("Tambah", kodGaji.Kod + " - " + kodGaji.Perihal, kodGaji.Kod, 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

                }
            }
            else
            {
                TempData[SD.Error] = "Kod ini telah wujud..!";
            }

            return View(kodGaji);
        }

        // GET: JSuKodGaji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KodGaji = await _context.JSuKodGaji.FindAsync(id);
            if (KodGaji == null)
            {
                return NotFound();
            }
            return View(KodGaji);
        }

        // POST: JSuKodGaji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Kod,Perihal")] JSuKodGaji kodGaji,int FlJenis) 
        {
            if (id != kodGaji.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.JSuKodGaji.FirstOrDefaultAsync(x => x.ID == kodGaji.ID);
                    var kodAsal = objAsal.Kod;
                    var perihalAsal = objAsal.Perihal;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    kodGaji.UserId = objAsal.UserId;
                    kodGaji.TarMasuk = objAsal.TarMasuk;
                    kodGaji.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    kodGaji.UserIdKemaskini = user.UserName;
                    kodGaji.TarKemaskini = DateTime.Now;
                    kodGaji.SuPekerjaKemaskiniId = pekerjaId;
                    kodGaji.FlJenis = FlJenis;

                    _context.Update(kodGaji);

                    await AddLogAsync("Ubah", kodAsal + " -> " + kodGaji.Kod + ", "
                        + perihalAsal + " -> " + kodGaji.Perihal + ", ", kodGaji.Kod, id, 0, pekerjaId);

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KodGajiExists(kodGaji.ID))
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
            return View(kodGaji);
        }

        // GET: JSuKodGaji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kodGaji = await _context.JSuKodGaji
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kodGaji == null)
            {
                return NotFound();
            }

            return View(kodGaji);
        }

        // POST: JSuKodGaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var kodGaji = await _context.JSuKodGaji.FindAsync(id);

            kodGaji.UserIdKemaskini = user.UserName;
            kodGaji.TarKemaskini = DateTime.Now;
            kodGaji.SuPekerjaKemaskiniId = pekerjaId;

            _context.JSuKodGaji.Remove(kodGaji);
            await AddLogAsync("Hapus", kodGaji.Kod + " - " + kodGaji.Perihal, kodGaji.Kod, id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.JSuKodGaji.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ID == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.JSuKodGaji.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.Kod + " - " + obj.Perihal, obj.Kod, id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }
        private bool KodGajiExists(int id)
        {
            return _context.JSuKodGaji.Any(e => e.ID == id);
        }

        private bool KodGajiExists(string kod)
        {
            return _context.JSuKodGaji.Any(e => e.Kod == kod);
        }
    }
}
