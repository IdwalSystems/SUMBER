﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules;
using SUMBER.Models.Modules.Cart;
using SUMBER.Models.Modules.IRepository;
using SUMBER.Models.Sumber;

namespace SUMBER.Controllers
{
    [Authorize(Roles = "SuperAdmin,Supervisor")]
    public class SuPekerjaController : Controller
    {
        public const string modul = "DF001";
        public const string namamodul = "Daftar Anggota";

        private readonly ApplicationDbContext _context;
        private readonly AppLogIRepository<AppLog, int> _appLog;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<SuPekerja, int, string> _suPekerjaRepo;
        private readonly IRepository<JNegeri, int, string> _jNegeriRepo;
        private readonly IRepository<JAgama, int, string> _jAgamaRepo;
        private readonly IRepository<JBangsa, int, string> _jBangsaRepo;
        private readonly ListViewIRepository<SuTanggunganPekerja, int> _suTanggunganRepo;
        private readonly IRepository<JCaraBayar, int, string> _jCaraBayarRepo;
        private readonly IRepository<JSuTarafJawatan, int, string> _jSuTarafJawatanRepo;
        private readonly ListViewIRepository<SuProfilGaji, int> _suProfilGajiRepo;
        private CartPekerja _cart;

        public SuPekerjaController(
            ApplicationDbContext context,
            AppLogIRepository<AppLog, int> appLog,
            UserManager<IdentityUser> userManager,
            IRepository<SuPekerja, int, string> suPekerjaRepo,
            IRepository<JNegeri, int, string> jNegeriRepo,
            IRepository<JAgama, int, string> jAgamaRepo,
            IRepository<JBangsa, int, string> jBangsaRepo,
            ListViewIRepository<SuTanggunganPekerja, int> suTanggunganRepo,
            IRepository<JCaraBayar, int, string> jCaraBayarRepo,
            IRepository<JSuTarafJawatan, int, string> jSuTarafJawatanRepo,
            ListViewIRepository<SuProfilGaji, int> suProfilGajiRepo,
            CartPekerja cart
            )
        {
            _context = context;
            _appLog = appLog;
            _userManager = userManager;
            _suPekerjaRepo = suPekerjaRepo;
            _jNegeriRepo = jNegeriRepo;
            _jAgamaRepo = jAgamaRepo;
            _jBangsaRepo = jBangsaRepo;
            _suTanggunganRepo = suTanggunganRepo;
            _jCaraBayarRepo = jCaraBayarRepo;
            _jSuTarafJawatanRepo = jSuTarafJawatanRepo;
            _suProfilGajiRepo = suProfilGajiRepo;
            _cart = cart;
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
            List<JNegeri> JNegeriList = _context.JNegeri.OrderBy(b => b.Kod).ToList();
            ViewBag.JNegeri = JNegeriList;

            List<JBank> JBankList = _context.JBank.OrderBy(b => b.Kod).ToList();
            ViewBag.JBank = JBankList;

            List<JAgama> JAgamaList = _context.JAgama.OrderBy(b => b.Perihal).ToList();
            ViewBag.JAgama = JAgamaList;

            List<JBangsa> JBangsaList = _context.JBangsa.OrderBy(b => b.Perihal).ToList();
            ViewBag.JBangsa = JBangsaList;

            List<JCaraBayar> JCaraBayarList = _context.JCaraBayar.OrderBy(b => b.Kod).ToList();
            ViewBag.JCaraBayar = JCaraBayarList;

            List<JSuTarafJawatan> JSuTarafJawatanList = _context.JSuTarafJawatan.OrderBy(b => b.Kod).ToList();
            ViewBag.JSuTarafJawatan = JSuTarafJawatanList;

            List<SuProfilGaji> SuProfilGajiList = _context.SuProfilGaji.OrderBy(b => b.JSuKodGajiId).ToList();
            ViewBag.SuProfilGaji = SuProfilGajiList;

            List<JSuKodGaji> JSuKodGajiList = _context.JSuKodGaji.OrderBy(b => b.Kod).ToList();
            ViewBag.JSuKodGaji = JSuKodGajiList;
        }

        private string GetNoGaji()
        {
            var suP = _suPekerjaRepo.GetAllIncludeDeletedItems()
                .Result
                .OrderByDescending(s => s.NoGaji).FirstOrDefault();
            int no = 0;
            if (suP != null)
            {
                if (int.TryParse(suP.NoGaji, out no))
                {
                    no += 1;
                }
            }
            else
            {
                no = 1;
            }
            return no.ToString("D5");
        }

        private void PopulateTable(int? id)
        {
            List<SuTanggunganPekerja> suTanggungan = _context.SuTanggunganPekerja.Where(b => b.SuPekerjaId == id).ToList();
            ViewBag.suTanggungan = suTanggungan;

            //List<SuProfilGaji> suProfilGaji = _context.SuProfilGaji.Where(b => b.SuPekerjaId == id).ToList();
            //ViewBag.suProfilGaji = suProfilGaji;

            List<SuProfilGaji> table1 = _context.SuProfilGaji
                .Include(b => b.JSuKodGaji)
                .Where(b => b.SuPekerjaId == id)
                .OrderBy(b => b.Id)
                .ToList();
            ViewBag.suProfilGaji = table1;
        }

        private JsonResult CartEmpty()
        {
            try
            {
                _cart.Clear1();

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }
        private void PopulateCart(SuPekerja suPekerja)
        {
            List<SuTanggunganPekerja> suTanggungan = _context.SuTanggunganPekerja
                .Where(b => b.SuPekerjaId == suPekerja.Id)
                .ToList();
            foreach (SuTanggunganPekerja suT in suTanggungan)
            {
                _cart.AddItem1(
                    suT.SuPekerjaId,
                    suT.Nama,
                    suT.Hubungan,
                    suT.NoKP
                    );
            }

            List<SuProfilGaji> suProfilGaji = _context.SuProfilGaji
                .Where(b => b.SuPekerjaId == suPekerja.Id)
                .ToList();
            foreach (SuProfilGaji suPG in suProfilGaji)
            {
                _cart.AddItem2(
                    suPG.SuPekerjaId,
                    suPG.JSuKodGajiId,
                    suPG.Elaun,
                    suPG.Potongan,
                    suPG.FlKWSP
                    );
            }

        }

        [Authorize(Policy = "DF001")]
        // GET: SuPekerja
        public async Task<IActionResult> Index()
        {
            var suPekerja = await _suPekerjaRepo.GetAll();

            if (User.IsInRole("SuperAdmin"))
            {
                suPekerja = await _suPekerjaRepo.GetAllIncludeDeletedItems();
            }
            return View(suPekerja);
        }

        // GET: SuPekerja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suPekerja = await _suPekerjaRepo.GetById((int)id);
            if (suPekerja == null)
            {
                return NotFound();
            }

            PopulateList();
            PopulateTable(id);
            return View(suPekerja);
        }

        [Authorize(Policy = "DF001C")]
        // GET: SuPekerja/Create
        public IActionResult Create()
        {
            ViewBag.nogaji = GetNoGaji();
            PopulateList();
            CartEmpty();
            return View();
        }

        [Authorize(Policy = "DF001C")]
        // POST: SuPekerja/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuPekerja suPekerja)
        {
            var username = User.FindFirstValue(ClaimTypes.Name).Substring(0, 15);

            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            SuPekerja m = new SuPekerja();
            if (ICPekerjaExists(suPekerja.NoKp) == false)
            {
                if (AkaunPekerjaExists(suPekerja.NoAkaunBank) == false)
                {
                    if (EmelPekerjaExists(suPekerja.Emel) == false)
                    {
                        if (suPekerja.Emel != null)
                        {
                            if (ModelState.IsValid)
                            {
                                //string noRujukan = GetKod(akJurnal.JKWId);
                                if (suPekerja != null)
                                {
                                    m.NoGaji = GetNoGaji();
                                    m.Nama = suPekerja.Nama?.ToUpper() ?? null;
                                    m.NoKp = suPekerja.NoKp;
                                    m.Alamat1 = suPekerja.Alamat1?.ToUpper() ?? null;
                                    m.Alamat2 = suPekerja.Alamat2?.ToUpper() ?? null;
                                    m.Alamat3 = suPekerja.Alamat3?.ToUpper() ?? null;
                                    m.Poskod = suPekerja.Poskod;
                                    m.Bandar = suPekerja.Bandar?.ToUpper() ?? null;
                                    m.JNegeriId = suPekerja.JNegeriId;
                                    m.JBankId = suPekerja.JBankId;
                                    m.Jawatan = suPekerja.Jawatan?.ToUpper() ?? null;
                                    //m.TelefonRumah = suPekerja.TelefonRumah;
                                    //m.TelefonBimbit = suPekerja.TelefonBimbit;
                                    m.Emel = suPekerja.Emel;
                                    m.StatusKahwin = suPekerja.StatusKahwin;
                                    m.BilAnak = suPekerja.BilAnak;
                                    //m.GajiPokok = suPekerja.GajiPokok;
                                    //m.TarikhMasukKerja = suPekerja.TarikhMasukKerja;
                                    //m.TarikhBerhentiKerja = suPekerja.TarikhBerhentiKerja;
                                    //m.TarikhPencen = suPekerja.TarikhPencen;
                                    //m.JAgamaId = suPekerja.JAgamaId;
                                    //m.JBangsaId = suPekerja.JBangsaId;
                                    //m.JCaraBayarId = suPekerja.JCaraBayarId;
                                    m.JSuTarafJawatan = suPekerja.JSuTarafJawatan;
                                    m.NoAkaunBank = suPekerja.NoAkaunBank;
                                    m.UserId = username;
                                    m.TarMasuk = DateTime.Now;
                                    m.SuPekerjaMasukId = pekerjaId;

                                    m.SuTanggungan = _cart.Lines1.ToArray();
                                    m.SuProfilGaji = _cart.Lines2.ToArray();

                                    await _suPekerjaRepo.Insert(m);

                                    //insert applog
                                    await AddLogAsync("Tambah", m.NoGaji + " - " + suPekerja.NoKp, m.NoGaji, 0, 0, pekerjaId);
                                    //insert applog end

                                    //await AddLogAsync("Tambah", noRujukan, kredit);
                                    await _context.SaveChangesAsync();

                                    CartEmpty();
                                    TempData[SD.Success] = "Maklumat berjaya ditambah. No Gaji yang didaftar adalah " + m.NoGaji;
                                    return RedirectToAction(nameof(Index));
                                }
                                //_context.Add(suPekerja);
                                //await _context.SaveChangesAsync();
                                //return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                    else
                    {
                        TempData[SD.Error] = "Emel ini telah wujud..!";
                    }

                }
                else
                {
                    TempData[SD.Error] = "No Akaun ini telah wujud..!";
                }

            }
            else
            {
                TempData[SD.Error] = "No Kad Pengenalan ini telah wujud..!";
            }

            ViewBag.nogaji = GetNoGaji();
            PopulateList();
            return View(suPekerja);
        }

        [Authorize(Policy = "DF001E")]
        // GET: SuPekerja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suPekerja = await _suPekerjaRepo.GetById((int)id);
            if (suPekerja == null)
            {
                return NotFound();
            }

            CartEmpty();
            PopulateList();
            PopulateTable(id);
            PopulateCart(suPekerja);
            return View(suPekerja);
        }

        [Authorize(Policy = "DF001E")]
        // POST: SuPekerja/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SuPekerja suPekerja)
        {
            if (id != suPekerja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

                    SuPekerja dataAsal = await _suPekerjaRepo.GetById(id);

                    // list of input that cannot be change
                    suPekerja.Emel = dataAsal.Emel;
                    suPekerja.TarMasuk = dataAsal.TarMasuk;
                    suPekerja.UserId = dataAsal.UserId;
                    suPekerja.NoKp = dataAsal.NoKp;
                    suPekerja.NoGaji = dataAsal.NoGaji;
                    var noAkaunAsal = dataAsal.NoAkaunBank;
                    var namaAsal = dataAsal.Nama;
                    suPekerja.SuPekerjaMasukId = dataAsal.SuPekerjaMasukId;
                    // list of input that cannot be change end

                    // list of input to uppercase
                    suPekerja.Nama = suPekerja.Nama?.ToUpper() ?? null;
                    suPekerja.Alamat1 = suPekerja.Alamat1?.ToUpper() ?? null;
                    suPekerja.Alamat2 = suPekerja.Alamat2?.ToUpper() ?? null;
                    suPekerja.Alamat3 = suPekerja.Alamat3?.ToUpper() ?? null;
                    suPekerja.Bandar = suPekerja.Bandar?.ToUpper() ?? null;
                    suPekerja.Jawatan = suPekerja.Jawatan?.ToUpper() ?? null;
                    // list of input to uppercase end

                    foreach (SuProfilGaji item in dataAsal.SuProfilGaji)
                    {
                        var model = _context.SuProfilGaji.FirstOrDefault(b => b.Id == item.Id);
                        if (model != null)
                        {
                            _context.Remove(model);
                        }
                    }
                    await _context.SaveChangesAsync();
                    _context.Entry(dataAsal).State = EntityState.Detached;

                    suPekerja.SuProfilGaji = _cart.Lines2.ToList();

                    suPekerja.UserIdKemaskini = user.UserName;
                    suPekerja.TarKemaskini = DateTime.Now;
                    suPekerja.SuPekerjaKemaskiniId = pekerjaId;

                    _context.Update(suPekerja);

                    //insert applog
                    if (namaAsal != suPekerja.Nama || noAkaunAsal != suPekerja.NoAkaunBank)
                    {
                        await AddLogAsync("Ubah", namaAsal + " -> " + suPekerja.Nama
                            + ", " + noAkaunAsal + " -> " + suPekerja.NoAkaunBank, suPekerja.NoGaji, id, 0, pekerjaId);
                    }
                    else
                    {
                        await AddLogAsync("Ubah", "Ubah Data", suPekerja.NoGaji, id, 0, pekerjaId);
                    }
                    //insert applog end

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuPekerjaExists(suPekerja.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                CartEmpty();
                TempData[SD.Success] = "Data berjaya diubah..!";
                return RedirectToAction(nameof(Index));

            }
            PopulateList();
            PopulateTable(id);
            return View(suPekerja);
        }

        [Authorize(Policy = "DF001D")]
        // GET: SuPekerja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suPekerja = await _suPekerjaRepo.GetById((int)id);
            PopulateTable(id);
            if (suPekerja == null)
            {
                return NotFound();
            }

            return View(suPekerja);
        }

        [Authorize(Policy = "DF001D")]
        // POST: SuPekerja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            var suPekerja = await _context.SuPekerja.FindAsync(id);

            suPekerja.UserIdKemaskini = user.UserName;
            suPekerja.TarKemaskini = DateTime.Now;
            suPekerja.SuPekerjaKemaskiniId = pekerjaId;

            _context.SuPekerja.Remove(suPekerja);
            await AddLogAsync("Hapus", suPekerja.NoKp + " - " + suPekerja.NoAkaunBank, suPekerja.NoGaji, id, 0, pekerjaId);
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dihapuskan..!";
            return RedirectToAction(nameof(Index));

        }

        private bool SuPekerjaExists(int id)
        {
            return _context.SuPekerja.Any(e => e.Id == id);
        }

        public JsonResult SaveTanggungan(SuTanggunganPekerja tanggungan)
        {
            try
            {
                if (tanggungan != null)
                {
                    _cart.AddItem1(
                        tanggungan.SuPekerjaId,
                        tanggungan.Nama,
                        tanggungan.Hubungan,
                        tanggungan.NoKP
                        );
                }
                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }
        public JsonResult RemoveTanggungan(SuTanggunganPekerja tanggungan)
        {
            try
            {
                if (tanggungan != null)
                {
                    _cart.RemoveItem1(tanggungan.NoKP);
                }
                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public async Task<JsonResult> InsertUpdateSuTanggungan(SuTanggunganPekerja suTanggungan)
        {
            try
            {
                if (suTanggungan != null)
                {
                    await _suTanggunganRepo.Insert(suTanggungan);

                    SuPekerja suPekerja = await _suPekerjaRepo.GetById(suTanggungan.SuPekerjaId);

                    if (suTanggungan.Hubungan == "ANAK")
                    {
                        suPekerja.BilAnak++;
                    }

                    await _suPekerjaRepo.Update(suPekerja);
                    await _context.SaveChangesAsync();
                }
                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public async Task<JsonResult> UpdateSuTanggungan(SuTanggunganPekerja suTanggungan)
        {
            try
            {
                SuTanggunganPekerja data = await _suTanggunganRepo.GetBy2Id(suTanggungan.SuPekerjaId, suTanggungan.Id);
                return Json(new { result = "OK", record = data });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public async Task<JsonResult> SaveUpdateSuTanggungan(SuTanggunganPekerja suTanggungan)
        {
            try
            {
                _cart.Clear1();

                SuTanggunganPekerja suT = await _suTanggunganRepo.GetById(suTanggungan.Id);
                suT.Nama = suTanggungan.Nama;
                suT.Hubungan = suTanggungan.Hubungan;

                _context.SuTanggunganPekerja.Update(suT);
                await _context.SaveChangesAsync();

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public async Task<JsonResult> GetCart1(SuTanggunganPekerja suTanggungan)
        {
            try
            {
                SuPekerja data = await _context.SuPekerja
                    .Include(x => x.SuTanggungan)
                    .FirstOrDefaultAsync(x => x.Id == suTanggungan.SuPekerjaId);

                List<SuTanggunganPekerja> suT = data.SuTanggungan.ToList();

                foreach (SuTanggunganPekerja item in suT)
                {
                    _cart.AddItem1(item.SuPekerjaId, item.Nama, item.Hubungan, item.NoKP);
                }

                int bilanak = 0;
                foreach (var item in suT.Where(x => x.Hubungan == "ANAK"))
                {
                    bilanak++;
                }

                SuPekerja suPekerja = await _suPekerjaRepo.GetById(suTanggungan.SuPekerjaId);

                suPekerja.BilAnak = bilanak;

                await _suPekerjaRepo.Update(suPekerja);
                await _context.SaveChangesAsync();

                return Json(new { result = "OK", data = data });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public async Task<JsonResult> RemoveUpdateSuTanggungan(SuTanggunganPekerja suTanggungan)
        {
            try
            {
                if (suTanggungan != null)
                {
                    var suT = await _context.SuTanggunganPekerja.FirstOrDefaultAsync(
                        x => x.NoKP == suTanggungan.NoKP
                        && x.SuPekerjaId == suTanggungan.SuPekerjaId
                        && x.Id == suTanggungan.Id);
                    _context.SuTanggunganPekerja.Remove(suT);

                    SuPekerja suPekerja = await _suPekerjaRepo.GetById(suTanggungan.SuPekerjaId);

                    if (suTanggungan.Hubungan == "ANAK")
                    {
                        suPekerja.BilAnak--;
                    }

                    await _suPekerjaRepo.Update(suPekerja);
                    await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();
                }
                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        [Authorize(Policy = "DF001R")]
        public async Task<IActionResult> RollBack(int id)
        {
            var obj = await _suPekerjaRepo.GetByIdIncludeDeletedItems(id);
            var user = await _userManager.GetUserAsync(User);
            int? pekerjaId = _context.applicationUsers.Where(b => b.Id == user.Id).FirstOrDefault().SuPekerjaId;

            // Batal operation

            obj.FlHapus = 0;
            obj.UserIdKemaskini = user.UserName;
            obj.TarKemaskini = DateTime.Now;
            obj.SuPekerjaKemaskiniId = pekerjaId;

            _context.SuPekerja.Update(obj);

            // Batal operation end
            await AddLogAsync("Rollback", obj.NoKp + " - " + obj.NoAkaunBank, obj.NoGaji, id, 0, pekerjaId);

            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Data berjaya dikembalikan..!";
            return RedirectToAction(nameof(Index));
        }

        private bool ICPekerjaExists(string kod)
        {
            return _context.SuPekerja.Any(e => e.NoKp == kod);
        }

        private bool AkaunPekerjaExists(string kod)
        {
            return _context.SuPekerja.Any(e => e.NoAkaunBank == kod);
        }

        private bool EmelPekerjaExists(string kod)
        {
            return _context.SuPekerja.Any(e => e.Emel == kod);
        }

        // function  json Create
        public JsonResult GetCarta(int id)
        {
            try
            {
                var result = _context.JSuKodGaji.Where(b => b.ID == id).FirstOrDefault();

                //var bahagian = _context.JBahagian.FirstOrDefault(b => b.Id == id2);

                return Json(new { result = "OK", record = result });
            }
            catch (Exception ex)
            {
                return Json(new { result = "Error", message = ex.Message });
            }

        }

        public JsonResult SaveSuProfilGaji(
            SuProfilGaji suProfilGaji,
            int suPekerjaId,
            int jSuKodGajiId,
            decimal elaun,
            decimal potongan,
            int flKWSP)
        {

            try
            {
                if (suProfilGaji != null)
                {

                    var cart = _cart.Lines2
                        .FirstOrDefault(b => b.JSuKodGajiId == suProfilGaji.JSuKodGajiId);

                    if (cart != null)
                    {
                        return Json(new { result = "ERROR", message = "Kod Gaji telah wujud." });
                    }
                    _cart.AddItem2(suProfilGaji.SuPekerjaId,
                                suProfilGaji.JSuKodGajiId,
                                suProfilGaji.Elaun,
                                suProfilGaji.Potongan,
                                suProfilGaji.FlKWSP
                                );

                }

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        public JsonResult RemoveSuProfilGaji(SuProfilGaji suProfilGaji)
        {

            try
            {
                if (suProfilGaji != null)
                {

                    _cart.RemoveItem2(suProfilGaji.JSuKodGajiId);
                }

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

        // get an item from cart suProfilGaji
        public JsonResult GetAnItemCartSuProfilGaji(SuProfilGaji suProfilGaji)
        {

            try
            {
                SuProfilGaji data = _cart.Lines2.Where(x => x.JSuKodGajiId == suProfilGaji.JSuKodGajiId).FirstOrDefault();

                return Json(new { result = "OK", record = data });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }
        // get an item from cart SuProfilGaji end

        //save cart SuProfilGaji
        public JsonResult SaveCartSuProfilGaji(
            SuProfilGaji suProfilGaji
            )
        {
            try
            {

                var suPG = _cart.Lines2.Where(x => x.JSuKodGajiId == suProfilGaji.JSuKodGajiId).FirstOrDefault();

                if (suPG != null)
                {

                    _cart.RemoveItem2(suPG.JSuKodGajiId);

                    _cart.AddItem2(suProfilGaji.SuPekerjaId,
                                    suProfilGaji.JSuKodGajiId,
                                    suProfilGaji.Elaun,
                                    suProfilGaji.Potongan,
                                    suProfilGaji.FlKWSP
                                    );
                }

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }
        //save cart akPOLaras1 end

        // get all item from cart akPOLaras1
        public JsonResult GetAllItemCartSuProfilGaji()
        {

            try
            {
                List<SuProfilGaji> data = _cart.Lines2.ToList();

                foreach (SuProfilGaji item in data)
                {
                    var jSuKodGaji = _context.JSuKodGaji.Find(item.JSuKodGajiId);

                    item.JSuKodGaji = jSuKodGaji;

                }

                return Json(new { result = "OK", record = data });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }
        // get all item from cart akPOLaras1 end

        // json empty Cart controller
        [HttpPost]
        public JsonResult JsonEmptyCart1()
        {
            try
            {
                CartEmpty1();

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "Error", message = ex.Message });
            }
        }
        // json empty cart end

        public JsonResult CartEmpty1()
        {
            try
            {
                _cart.Clear2();

                return Json(new { result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", message = ex.Message });
            }
        }

    }

}
