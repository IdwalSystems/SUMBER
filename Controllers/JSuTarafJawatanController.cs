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
    public class JSuTarafJawatanController : Controller
    {
        public const string modul = "JD014";
        public const string namamodul = "Jadual Taraf Jawatan";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppLogIRepository<AppLog, int> _appLog;

        public JSuTarafJawatanController(ApplicationDbContext context, 
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

        // GET: JSuTarafJawatan
        public async Task<IActionResult> Index()
        {
            var obj = await _context.JSuTarafJawatan.ToListAsync();

            if (User.IsInRole("SuperAdmin"))
            {
                obj = await _context.JSuTarafJawatan.IgnoreQueryFilters().ToListAsync();
            }

            return View(obj);
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
        public async Task<IActionResult> Create([Bind("Id,Kod,Perihal")] JSuTarafJawatan jSuTarafJawatan)
        {
            if (KodTarafJawatanExists(jSuTarafJawatan.Kod) == false)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    jSuTarafJawatan.UserId = user.UserName;
                    jSuTarafJawatan.TarMasuk = DateTime.Now;
                    jSuTarafJawatan.SuPekerjaMasukId = pekerjaId;

                    _context.Add(jSuTarafJawatan);
                    await AddLogAsync("Tambah", jSuTarafJawatan.Kod + " - " + jSuTarafJawatan.Perihal, jSuTarafJawatan.Kod, 0, 0, pekerjaId);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Data berjaya ditambah..!";
                    return RedirectToAction(nameof(Index));

        }
            }
            else
            {
                TempData[SD.Error] = "Kod ini telah wujud..!";
            }

            return View(jSuTarafJawatan);
        }

        private bool KodTarafJawatanExists(string kod)
        {
            return _context.JSuTarafJawatan.Any(e => e.Kod == kod);
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
