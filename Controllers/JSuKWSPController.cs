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
    public class JSuKWSPController : Controller
    {
        public const string modul = "JD018";
        public const string namamodul = "Jadual KWSP";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;

        public JSuKWSPController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            AppLogIRepository<AppLog, int> appLog)
        {
            _context = context;
            _userManager = userManager;
            _appLog = appLog;
        }

        // GET: JSuKWSP

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

            var obj = await _context.JSuKWSP.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.JSuKWSP.IgnoreQueryFilters().ToListAsync();
            }

            return View(obj);

        }

        // GET: JSuKWSP/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suKWSP = await _context.JSuKWSP
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suKWSP == null)
            {
                return NotFound();
            }

            return View(suKWSP);
        }

        // GET: JSuKWSP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JSuKWSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GajiAwal,GajiAkhir,PotongPekerja,PotongMajikan")] JSuKWSP suKWSP, decimal GajiAwal, decimal GajiAkhir, decimal PotongPekerja, decimal PotongMajikan)
        {
            //if (IdsuKWSPExists(suKWSP.Id) == false)
            //{
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                suKWSP.GajiAwal = GajiAwal;
                suKWSP.GajiAkhir = GajiAkhir;
                suKWSP.PotongPekerja = PotongPekerja;
                suKWSP.PotongMajikan = PotongMajikan;
                suKWSP.UserId = user.UserName;
                suKWSP.TarMasuk = DateTime.Now;
                suKWSP.SuPekerjaMasukId = pekerjaId;

                _context.Add(suKWSP);
                await AddLogAsync("Tambah", suKWSP.GajiAwal + " - " + suKWSP.GajiAkhir, suKWSP.GajiAwal.ToString(), 0, 0, pekerjaId);
                await _context.SaveChangesAsync();
                TempData[SD.Success] = "Data berjaya ditambah..!";
                return RedirectToAction(nameof(Index));

            }

            return View(suKWSP);
        }

        // GET: JSuKWSP/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuKWSP = await _context.JSuKWSP.FindAsync(id);
            if (jSuKWSP == null)
            {
                return NotFound();
            }
            return View(jSuKWSP);
        }

        // POST: JSuKWSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GajiAwal,GajiAkhir,PotongPekerja,PotongMajikan")] JSuKWSP jSuKWSP, decimal GajiAwal, decimal GajiAkhir, decimal PotongPekerja, decimal PotongMajikan)
        {
            if (id != jSuKWSP.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.JSuKWSP.FirstOrDefaultAsync(x => x.Id == jSuKWSP.Id);
                    var GajiAwalAsal = objAsal.GajiAwal;
                    var GajiAkhirAsal = objAsal.GajiAkhir;
                    var PotongPekerjaAsal = objAsal.PotongPekerja;
                    var PotongMajikanAsal = objAsal.PotongMajikan;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    //jSuKWSP.GajiAkhir = objAsal.GajiAkhir;
                    //jSuKWSP.PotongPekerja = objAsal.PotongPekerja;
                    //jSuKWSP.PotongMajikan = objAsal.PotongMajikan;
                    jSuKWSP.UserId = objAsal.UserId;
                    jSuKWSP.TarMasuk = objAsal.TarMasuk;
                    jSuKWSP.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    jSuKWSP.UserIdKemaskini = user.UserName;
                    jSuKWSP.TarKemaskini = DateTime.Now;
                    jSuKWSP.SuPekerjaKemaskiniId = pekerjaId;

                    _context.Update(jSuKWSP);

                    await AddLogAsync("Ubah", GajiAwalAsal + " -> " + jSuKWSP.GajiAwal + ", "
                        + GajiAkhirAsal + " -> " + jSuKWSP.GajiAkhir + ", "
                        + PotongPekerjaAsal + " -> " + jSuKWSP.PotongPekerja + ", "
                        + PotongMajikanAsal + " -> " + jSuKWSP.PotongMajikan + ", ","", id, 0, pekerjaId);

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSuKWSPExists(jSuKWSP.Id))
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
            return View(jSuKWSP);
        }

        // GET: JSuKWSP/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuKWSP = await _context.JSuKWSP
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jSuKWSP == null)
            {
                return NotFound();
            }

            return View(jSuKWSP);
        }

        // POST: JSuKWSP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var jSuKWSP = await _context.JSuKWSP.FindAsync(id);

            jSuKWSP.UserIdKemaskini = user.UserName;
            jSuKWSP.TarKemaskini = DateTime.Now;
            jSuKWSP.SuPekerjaKemaskiniId = pekerjaId;

            _context.JSuKWSP.Remove(jSuKWSP);
            await AddLogAsync("Hapus", jSuKWSP.Id + " - " + jSuKWSP.Id, jSuKWSP.Id.ToString(), id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.JSuKWSP.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.JSuKWSP.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.Id + " - " + obj.Id, obj.Id.ToString(), id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }

        private bool JSuKWSPExists(int id)
        {
            return _context.JSuKWSP.Any(e => e.Id == id);
        }

    }
}