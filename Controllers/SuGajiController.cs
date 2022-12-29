using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules;
using SUMBER.Models.Modules.IRepository;
using SUMBER.Models.Sumber;

namespace SUMBER.Controllers
{
    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class SuGajiController : Controller
    {
        public const string modul = "DF007";
        public const string namamodul = "Daftar Gaji";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;
        private readonly IRepository<SuPekerja, int, string> _suPekerjaRepo;
        private readonly IRepository<JSuKodGaji, int, string> _jSuKodGajiRepo;

        public SuGajiController(
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

        // GET: SuGaji

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

        public async Task<IActionResult> Index()
        {

            var obj = await _context.SuGaji.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.SuGaji.IgnoreQueryFilters().ToListAsync();
            }

            PopulateList();
            return View(obj);

            //var applicationDbContext = _context.SuGaji.Include(s => s.JSuKodGaji).Include(s => s.SuPekerja);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: SuGaji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suGaji = await _context.SuGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suGaji == null)
            {
                return NotFound();
            }

            PopulateList();
            return View(suGaji);
        }

        // GET: SuGaji/Create
        public IActionResult Create()
        {
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod");
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel");
            PopulateList();
            return View();
        }

        // POST: SuGaji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,SuPekerjaId,JSuKodGajiId,Amaun")] SuGaji suGaji, int SuPekerjaId, int JSuKodGajiId, decimal Amaun)
        {
            if (SuPekerjaIdSuGajiExists(suGaji.SuPekerjaId) == false)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    suGaji.SuPekerjaId = SuPekerjaId;
                    suGaji.JSuKodGajiId = JSuKodGajiId;
                    suGaji.Amaun = Amaun;
                    suGaji.UserId = user.UserName;
                    suGaji.TarMasuk = DateTime.Now;
                    suGaji.SuPekerjaMasukId = pekerjaId;

                    _context.Add(suGaji);
                    await AddLogAsync("Tambah", suGaji.SuPekerjaId + " - " + suGaji.JSuKodGajiId, suGaji.SuPekerjaId.ToString(), 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

                }
            }
            else
            {
                TempData[SD.Error] = "Anggota ini telah wujud..!";
            }
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suGaji.SuPekerjaId);
            PopulateList();
            return View(suGaji);
        }

        // GET: SuGaji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suGaji = await _context.SuGaji.FindAsync(id);
            if (suGaji == null)
            {
                return NotFound();
            }
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suGaji.SuPekerjaId);
            PopulateList();
            return View(suGaji);
        }

        // POST: SuGaji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuPekerjaId,JSuKodGajiId,Amaun")] SuGaji suGaji, int SuPekerjaId, int JSuKodGajiId, decimal Amaun)
        {
            if (id != suGaji.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var objAsal = await _context.SuGaji.FirstOrDefaultAsync(x => x.Id == suGaji.Id);
                    var SuPekerjaIdAsal = objAsal.SuPekerjaId;
                    var JSuKodGajiIdAsal = objAsal.JSuKodGajiId;
                    var AmaunAsal = objAsal.Amaun;
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    suGaji.UserId = objAsal.UserId;
                    suGaji.TarMasuk = objAsal.TarMasuk;
                    suGaji.SuPekerjaMasukId = objAsal.SuPekerjaMasukId;

                    _context.Entry(objAsal).State = EntityState.Detached;

                    suGaji.UserIdKemaskini = user.UserName;
                    suGaji.TarKemaskini = DateTime.Now;
                    suGaji.SuPekerjaKemaskiniId = pekerjaId;

                    _context.Update(suGaji);

                    await AddLogAsync("Ubah", SuPekerjaIdAsal + " -> " + suGaji.SuPekerjaId + ", "
                        + JSuKodGajiIdAsal + " -> " + suGaji.JSuKodGajiId + ", "
                        + AmaunAsal + " -> " + suGaji.Amaun + ", ", "", id, 0, pekerjaId);

                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya diubah..!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuGajiExists(suGaji.Id))
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
            //ViewData["JSuKodGajiId"] = new SelectList(_context.JSuKodGaji, "ID", "Kod", suGaji.JSuKodGajiId);
            //ViewData["SuPekerjaId"] = new SelectList(_context.SuPekerja, "Id", "Emel", suGaji.SuPekerjaId);
            PopulateList();
            return View(suGaji);
        }

        // GET: SuGaji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var suGaji = await _context.SuGaji
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suGaji == null)
            {
                return NotFound();
            }

            PopulateList();
            return View(suGaji);
        }

        // POST: SuGaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var suGaji = await _context.SuGaji.FindAsync(id);

            suGaji.UserIdKemaskini = user.UserName;
            suGaji.TarKemaskini = DateTime.Now;
            suGaji.SuPekerjaKemaskiniId = pekerjaId;

            _context.SuGaji.Remove(suGaji);
            await AddLogAsync("Hapus", suGaji.Id + " - " + suGaji.Id, suGaji.Id.ToString(), id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _context.SuGaji.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;
            obj.FlHapus = 0;
            _context.SuGaji.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.Id + " - " + obj.Id, obj.Id.ToString(), id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }

        private bool SuGajiExists(int id)
        {
            return _context.SuGaji.Any(e => e.Id == id);
        }

        private bool SuPekerjaIdSuGajiExists(int suPekerjaId)
        {
            return _context.SuGaji.Any(e => e.SuPekerjaId == suPekerjaId);
        }
    }
}
