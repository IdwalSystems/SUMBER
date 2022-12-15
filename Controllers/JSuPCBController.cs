using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using SUMBER.Models.Modules;
using SUMBER.Models.Sumber;
using System.Net.Mail;


namespace SUMBER.Controllers
{
    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class JSuPCBController : Controller
    {
        public const string modul = "JD017";
        public const string namamodul = "Jadual PCB";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;

        public JSuPCBController(ApplicationDbContext context,
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
           decimal bujang,
           decimal kahwin,
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

        // GET: JSuPCB
        public async Task<IActionResult> Index()
        {
            var obj = await _context.JSuPCB.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.JSuPCB.IgnoreQueryFilters().ToListAsync();
            }

            return View(obj);
        }

        // GET: JSuPCB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuPCB = await _context.JSuPCB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jSuPCB == null)
            {
                return NotFound();
            }

            return View(jSuPCB);
        }

        // GET: JSuPCB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JSuTarafJawatan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GajiAwal,GajiAkhir,Bujang,Kahwin")] JSuPCB jSuPCB, decimal gajiAwal, decimal gajiAkhir, int FlKategori, decimal bujang, decimal kahwin)
        {
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    jSuPCB.UserId = user.UserName;
                    jSuPCB.TarMasuk = DateTime.Now;
                    jSuPCB.SuPekerjaMasukId = pekerjaId;
                    jSuPCB.FlKategori = FlKategori;
                    jSuPCB.GajiAwal = gajiAwal;
                    jSuPCB.GajiAkhir = gajiAkhir;
                    jSuPCB.Bujang = bujang;
                    jSuPCB.Kahwin = kahwin;

                    _context.Add(jSuPCB);
                    await AddLogAsync("Tambah", jSuPCB.GajiAwal + " - " + jSuPCB.GajiAkhir, jSuPCB.Bujang, jSuPCB.Kahwin, " ", 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

                }

                return View(jSuPCB);
            }
        }

     

        private bool KodjSuPCBExists(int id)
        {
            return _context.JSuPCB.Any(e => e.Id == id);
        }

        // GET: JSuPCB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuPCB = await _context.JSuPCB.FindAsync(id);
            if (jSuPCB == null)
            {
                return NotFound();
            }

            return View(jSuPCB);
        }

        // POST: JSuPCB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GajiAwal,GajiAkhir,Bujang,Kahwin")] JSuPCB jSuPCB, decimal gajiAwal, decimal gajiAkhir, int FlKategori, decimal bujang, decimal kahwin)
        {
            if (id != jSuPCB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.JSuPCB.FirstOrDefaultAsync(x => x.Id == jSuPCB.Id);
                    var GajiAwalAsal = objAsal.GajiAwal;
                    var GajiAkhirAsal = objAsal.GajiAkhir;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    jSuPCB.UserId = objAsal.UserId;
                    jSuPCB.TarMasuk = objAsal.TarMasuk;
                    jSuPCB.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    jSuPCB.UserIdKemaskini = user.UserName;
                    jSuPCB.TarKemaskini = DateTime.Now;
                    jSuPCB.SuPekerjaKemaskiniId = pekerjaId;
                    jSuPCB.FlKategori = FlKategori;
                    jSuPCB.GajiAwal = gajiAwal;
                    jSuPCB.GajiAkhir = gajiAkhir;
                    jSuPCB.Bujang = bujang;
                    jSuPCB.Kahwin = kahwin;

                    _context.Update(jSuPCB);

                    if (GajiAwalAsal != jSuPCB.GajiAwal)
                    {
                        await AddLogAsync("Ubah", GajiAwalAsal + " -> " + jSuPCB.GajiAwal + ", "
                        + GajiAkhirAsal + " -> " + jSuPCB.GajiAkhir + ", ", jSuPCB.Bujang, jSuPCB.Kahwin, "", id, 0, pekerjaId);
                    }
                        

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSuPCBExists(jSuPCB.Id))
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
            return View(jSuPCB);
        }

     


        // GET: JSuPCB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jSuPCB = await _context.JSuPCB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jSuPCB == null)
            {
                return NotFound();
            }

            return View(jSuPCB);
        }

        // POST: JSuPCB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var jSuPCB = await _context.JSuPCB.FindAsync(id);

            jSuPCB.UserIdKemaskini = user.UserName;
            jSuPCB.TarKemaskini = DateTime.Now;
            jSuPCB.SuPekerjaKemaskiniId = pekerjaId;

            _context.JSuPCB.Remove(jSuPCB);
            await AddLogAsync("Hapus", jSuPCB.GajiAwal + " - " + jSuPCB.GajiAkhir, jSuPCB.Bujang, jSuPCB.Kahwin, "", id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.JSuPCB.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.JSuPCB.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.GajiAwal + " - " + obj.GajiAkhir, obj.Bujang, obj.Kahwin, "", id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }
        private bool JSuPCBExists(int id)
        {
            return _context.JSuPCB.Any(e => e.Id == id);
        }
    }
}

