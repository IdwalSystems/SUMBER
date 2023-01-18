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

namespace SUMBER.Controllers
{
    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class SuProfilGajiController : Controller
    {
        public const string modul = "DF007";
        public const string namamodul = "Daftar Gaji";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;
        private readonly IRepository<SuPekerja, int, string> _suPekerjaRepo;
        private readonly IRepository<JSuKodGaji, int, string> _jSuKodGajiRepo;

        public SuProfilGajiController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            AppLogIRepository<AppLog, int> appLog,
            IRepository<SuPekerja, int, string> suPekerjaRepo,
            IRepository<JSuKodGaji, int, string> jSuKodGajiRepo
            )
        {
            _context = context;
            _userManager = userManager;
            _appLog = appLog;
            _suPekerjaRepo = suPekerjaRepo;
            _jSuKodGajiRepo = jSuKodGajiRepo;
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

        private void PopulateList()
        {
            List<SuPekerja> SuPekerjaList = _context.SuPekerja.OrderBy(b => b.NoGaji).ToList();
            ViewBag.SuPekerja = SuPekerjaList;

            List<JSuKodGaji> JSuKodGajiList = _context.JSuKodGaji.OrderBy(b => b.Kod).ToList();
            ViewBag.JSuKodGaji = JSuKodGajiList;
        }

        // GET: SuProfilGaji
        public async Task<IActionResult> Index()
        {
            var obj = await _context.SuProfilGaji.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.SuProfilGaji.IgnoreQueryFilters().ToListAsync();
            }

            PopulateList();
            return View(obj);

            //var applicationDbContext = _context.SuProfilGaji.Include(s => s.JSuKodGaji).Include(s => s.SuPekerja);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: SuProfilGaji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suProfilGaji = await _context.SuProfilGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suProfilGaji == null)
            {
                return NotFound();
            }

            PopulateList();
            return View(suProfilGaji);
        }

        // GET: SuProfilGaji/Create
        public IActionResult Create()
        {
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod");
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel");
            PopulateList();
            return View();
        }

        // POST: SuProfilGaji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SuPekerjaId,JSuKodGajiId,Amaun")] SuProfilGaji suProfilGaji, int SuPekerjaId, int JSuKodGajiId, decimal Amaun)
        {
            if (SuPekerjaIdSuProfilGajiExists(suProfilGaji.SuPekerjaId) == false)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    suProfilGaji.SuPekerjaId = SuPekerjaId;
                    suProfilGaji.JSuKodGajiId = JSuKodGajiId;
                    suProfilGaji.UserId = user.UserName;
                    suProfilGaji.TarMasuk = DateTime.Now;
                    suProfilGaji.SuPekerjaMasukId = pekerjaId;

                    _context.Add(suProfilGaji);
                    await AddLogAsync("Tambah", suProfilGaji.SuPekerjaId + " - " + suProfilGaji.JSuKodGajiId, suProfilGaji.SuPekerjaId.ToString(), 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

                }
            }
            else
            {
                TempData[SD.Error] = "Anggota ini telah wujud..!";
            }
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suProfilGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suProfilGaji.SuPekerjaId);
            PopulateList();
            return View(suProfilGaji);
        }

        // GET: SuProfilGaji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suProfilGaji = await _context.SuProfilGaji.FindAsync(id);
            if (suProfilGaji == null)
            {
                return NotFound();
            }
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suProfilGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suProfilGaji.SuPekerjaId);
            PopulateList();
            return View(suProfilGaji);
        }

        // POST: SuProfilGaji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuPekerjaId,JSuKodGajiId,Amaun")] SuProfilGaji suProfilGaji, int SuPekerjaId, int JSuKodGajiId, decimal Amaun)
        {
            if (id != suProfilGaji.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.SuProfilGaji.FirstOrDefaultAsync(x => x.Id == suProfilGaji.Id);
                    var SuPekerjaIdAsal = objAsal.SuPekerjaId;
                    var JSuKodGajiIdAsal = objAsal.JSuKodGajiId;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    suProfilGaji.UserId = objAsal.UserId;
                    suProfilGaji.TarMasuk = objAsal.TarMasuk;
                    suProfilGaji.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    suProfilGaji.UserIdKemaskini = user.UserName;
                    suProfilGaji.TarKemaskini = DateTime.Now;
                    suProfilGaji.SuPekerjaKemaskiniId = pekerjaId;

                    _context.Update(suProfilGaji);

                    await AddLogAsync("Ubah", SuPekerjaIdAsal + " -> " + suProfilGaji.SuPekerjaId + ", "
                        + JSuKodGajiIdAsal + " -> " + suProfilGaji.JSuKodGajiId + ", "
                        + ", ", "", id, 0, pekerjaId);

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuProfilGajiExists(suProfilGaji.Id))
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
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suProfilGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suProfilGaji.SuPekerjaId);
            PopulateList();
            return View(suProfilGaji);
        }

        // GET: SuProfilGaji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suProfilGaji = await _context.SuProfilGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suProfilGaji == null)
            {
                return NotFound();
            }

            PopulateList();
            return View(suProfilGaji);
        }

        // POST: SuProfilGaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var suProfilGaji = await _context.SuProfilGaji.FindAsync(id);

            suProfilGaji.UserIdKemaskini = user.UserName;
            suProfilGaji.TarKemaskini = DateTime.Now;
            suProfilGaji.SuPekerjaKemaskiniId = pekerjaId;

            _context.SuProfilGaji.Remove(suProfilGaji);
            await AddLogAsync("Hapus", suProfilGaji.Id + " - " + suProfilGaji.Id, suProfilGaji.Id.ToString(), id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.SuProfilGaji.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.SuProfilGaji.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.Id + " - " + obj.Id, obj.Id.ToString(), id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }

        private bool SuProfilGajiExists(int id)
        {
            return _context.SuProfilGaji.Any(e => e.Id == id);
        }

        private bool SuPekerjaIdSuProfilGajiExists(int suPekerjaId)
        {
            return _context.SuProfilGaji.Any(e => e.SuPekerjaId == suPekerjaId);
        }
    }
}
