﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SUMBER.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using SUMBER.Models.Administration;
using Microsoft.AspNetCore.Identity;
using SUMBER.Models.Modules.IRepository;
using System.Dynamic;
using System.Threading.Tasks;
using SUMBER.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SUMBER.Models.Modules;
using SUMBER.Models.Modules.ViewModel;
using static SUMBER.Models.Modules.ViewModel.UserClaimsViewModel;
using Rotativa.AspNetCore;
using SUMBER.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;

namespace SUMBER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<AkPO, int, string> _poRepo;
        private readonly UserService _userService;

        public HomeController(
            ApplicationDbContext context,
            ILogger<HomeController> logger,  
            UserManager<IdentityUser> userManager,
            IRepository<AkPO, int, string> poRepo,
            UserService userService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _poRepo = poRepo;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            else
            {
                if(!User.IsInRole("SuperAdmin"))
                {
                    bool IsUnderMaintainance = _userService.IsUnderMaintainance();
                    if (IsUnderMaintainance == true)
                    {
                        return View("UnderMaintainance");
                    }
                }
                
                // Widget Status PO
                var akPO = await _context.AkPO
                    .Include(b => b.AkPembekal)
                    .Where(b=> b.FlPosting == 0)
                    .OrderByDescending(b=> b.Tarikh)
                    .ToListAsync();
                // filtering day balance
                var BenchDate = DateTime.Today.AddDays(-5);
                akPO = akPO.Where(b => b.Tarikh < BenchDate).ToList();

                // badge count
                int bilMore5Days = 0;
                int bilLess5Days = 0;
                foreach (var item in akPO)
                {
                    var bakiTarikh = (DateTime.Now - item.Tarikh).Days;
                    if (bakiTarikh > 14)
                    {
                        bilMore5Days++;
                    }
                    else
                    {
                        bilLess5Days++;
                    }
                }
                // badge count end
                // Widget Status PO end

                // Widget Status Nota Minta
                var akNotaMinta = await _context.AkNotaMinta
                    .Include(b => b.AkPembekal)
                    .Where(b => b.FlPosting == 0 )
                    .OrderByDescending(b => b.Tarikh)
                    .ToListAsync();

                // badge count
                int bilKewNM = 0;
                int bilLulusNM = 0;
                foreach (var item in akNotaMinta)
                {
                    if (item.NoSiri == null)
                    {
                        bilKewNM++;
                    } 
                    else
                    {
                        bilLulusNM++;
                    }
                }
                //badge count end
                // Widget Status PO end

                // Widget Status Pendahuluan Pelbagai
                var spPendahuluanPelbagai = await _context.SpPendahuluanPelbagai
                    .Include(b => b.SuPekerja)
                    .Where(b => b.FlPosting == 0)
                    .OrderByDescending(b => b.Tarikh)
                    .ToListAsync();

                // badge count
                int bilKewPP = 0;
                foreach (var item in spPendahuluanPelbagai)
                {
                    bilKewPP++;
                }
                //badge count end
                // Widget Status Pendahuluan Pelbagai end

                // Widget Status Pendahuluan Pelbagai
                var suProfil = await _context.SuProfil
                    .Include(b => b.JKW)
                    .Include(b => b.JBahagian)
                    .Where(b => b.FlPosting == 0)
                    .OrderByDescending(b => b.NoRujukan)
                    .ToListAsync();

                // badge count
                int bilKewP = 0;
                foreach (var item in suProfil)
                {
                    bilKewP++;
                }
                //badge count end
                // Widget Status Pendahuluan Pelbagai end

                List<JPenyemak> penyemak = _context.JPenyemak
                .Include(x => x.SuPekerja)
                .Where(x => x.IsNotaMinta == true).OrderBy(b => b.SuPekerja.Nama).ToList();
                ViewBag.JPenyemakNM = penyemak;

                List<JPelulus> pelulus = _context.JPelulus
                    .Include(x => x.SuPekerja)
                    .Where(x => x.IsNotaMinta == true).OrderBy(b => b.SuPekerja.Nama).ToList();
                ViewBag.JPelulusNM = pelulus;

                dynamic dyModel = new ExpandoObject();
                dyModel.AkPO = akPO;
                dyModel.bilMore5Days = bilMore5Days;
                dyModel.bilLess5Days = bilLess5Days;
                dyModel.AkNotaMinta = akNotaMinta;
                dyModel.bilKewNM = bilKewNM;
                dyModel.bilLulusNM = bilLulusNM;
                dyModel.SpPendahuluanPelbagai = spPendahuluanPelbagai;
                dyModel.bilKewPP = bilKewPP;
                dyModel.SuProfil = suProfil;
                dyModel.bilKewP = bilKewP;
                return View(dyModel);
            }
            
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            //test
            return View();
        }

        public IActionResult UnderMaintainance()
        {
            //test
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // get details of exception features
            // errormessage, Username, StackTrace etc.
            var contextException = HttpContext.Features.Get<IExceptionHandlerFeature>();
            // get details of request feature
            // path, url requested etc.
            var contextRequest = HttpContext.Features.Get<IHttpRequestFeature>();

            ExceptionLogger logger = new ExceptionLogger();

            var traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var IsExistLogger = _context.ExceptionLogger.FirstOrDefault(b => b.TraceIdentifier == traceId);
            
            // error handler logs for View
            if(IsExistLogger == null)
            {
                logger.LogTime = DateTime.Now;
                logger.UserName = HttpContext.User.Identity.Name;
                logger.TraceIdentifier = traceId;
                logger.ControllerName = ControllerContext.ActionDescriptor.DisplayName;
                logger.ExceptionMessage = contextException.Error.Message;
                logger.ExceptionStackTrace = contextException.Error.StackTrace;
                logger.ExceptionType = contextException.Error.GetType().FullName;
                logger.Source = contextException.Error.Source;
                logger.UrlRequest = contextRequest.RawTarget;

                _context.Add(logger);
                _context.SaveChanges();
            }


            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GlobalError()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"{code}";
            return View("~/Views/Shared/HandleError.cshtml");
        }

        // printing List of Carta
        [AllowAnonymous]
        public async Task<IActionResult> PrintPermohonanCapaian()
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel()
            {
                UserId = user.Id
            };

            foreach (Claim claim in ClaimStore.claimsList.OrderBy(b => b.Type))
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);

            }

            //string customSwitches = "--page-offset 0 --footer-center [page] / [toPage] --footer-font-size 6";

            return new ViewAsPdf("PermohonanCapaianPrintPDF", model)
            {
                PageMargins = { Left = 15, Bottom = 15, Right = 15, Top = 15 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = "--footer-center \"[page]/[toPage]\"" +
                        " --footer-line --footer-font-size \"7\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }
        // printing List of Carta end

    }
}
