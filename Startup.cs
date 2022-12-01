using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SUMBER.Data;
using SUMBER.Infrastructure;
using SUMBER.Models.Modules;
using SUMBER.Models.Modules.Cart.Session;
using SUMBER.Models.Modules.EFRepository;
using SUMBER.Models.Modules.IRepository;
using Rotativa.AspNetCore;
using System;
using SUMBER.Models.Sumber.EFRepository;
using SUMBER.Models.Sumber;

namespace SUMBER
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMemoryCache();

            services.AddTransient<UserService, UserService>();
            services.AddDbContext<ApplicationDbContext>(
                options=> {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    options.UseTriggers(triggerOptions =>
                    {
                        triggerOptions.AddTrigger<SoftDeleteTrigger>();
                    });
                });
            

            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider); ;

            services.Configure<IdentityOptions>(opt =>
                {
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireLowercase = true;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                }
            );
            services.ConfigureApplicationCookie(opt =>
            {
                opt.AccessDeniedPath = new PathString("/Home/Accessdenied");
                opt.ExpireTimeSpan = TimeSpan.FromSeconds(600);
                opt.LoginPath = "/Account/Login";
                opt.SlidingExpiration = true;
            });

            services.AddTransient<IRepository<AkBank, int, string>, AkBankRepository>();
            services.AddTransient<IRepository<JKW, int, string>, JKWRepository>();
            services.AddTransient<IRepository<JBank, int, string>, JBankRepository>();
            services.AddTransient<IRepository<JNegeri, int, string>, JNegeriRepository>();
            services.AddTransient<IRepository<AkCarta, int, string>, AkCartaRepository>();
            services.AddTransient<IRepository<AkAkaun, int, string>, AkAkaunRepository>();
            services.AddTransient<IRepository<AkTerima, int, string>, AkTerimaRepository>();
            services.AddTransient<ListViewIRepository<AkTerima1, int>, AkTerima1Repository>();
            services.AddTransient<ListViewIRepository<AkTerima2, int>, AkTerima2Repository>();
            services.AddTransient<IRepository<AkPO, int, string>, AkPORepository>();
            services.AddTransient<ListViewIRepository<AkPO1, int>, AkPO1Repository>();
            services.AddTransient<ListViewIRepository<AkPO2, int>, AkPO2Repository>();
            services.AddTransient<IRepository<AkInden, int, string>, AkIndenRepository>();
            services.AddTransient<ListViewIRepository<AkInden1, int>, AkInden1Repository>();
            services.AddTransient<ListViewIRepository<AkInden2, int>, AkInden2Repository>();
            services.AddTransient<IRepository<AkPOLaras, int, string>, AkPOLarasRepository>();
            services.AddTransient<ListViewIRepository<AkPOLaras1, int>, AkPOLaras1Repository>();
            services.AddTransient<ListViewIRepository<AkPOLaras2, int>, AkPOLaras2Repository>();
            services.AddTransient<IRepository<AkPembekal, int, string>, AkPembekalRepository>();
            services.AddTransient<IRepository<AkJurnal, int, string>, AkJurnalRepository>();
            services.AddTransient<ListViewIRepository<AkJurnal1, int>, AkJurnal1Repository>();
            services.AddTransient<IRepository<AkBelian, int, string>, AkBelianRepository>();
            services.AddTransient<ListViewIRepository<AkBelian1, int>, AkBelian1Repository>();
            services.AddTransient<ListViewIRepository<AkBelian2, int>, AkBelian2Repository>();
            services.AddTransient<AppLogIRepository<AppLog, int>, AppLogRepository>();
            services.AddTransient<IRepository<AkPV, int, string>, AkPVRepository>();
            services.AddTransient<ListViewIRepository<AkPV1, int>, AkPV1Repository>();
            services.AddTransient<ListViewIRepository<AkPV2, int>, AkPV2Repository>();
            services.AddTransient<ListViewIRepository<AkPVGanda, int>, AkPVGandaRepository>();
            services.AddTransient<IRepository<SuPekerja, int, string>, SuPekerjaRepository>();
            services.AddTransient<ListViewIRepository<SuTanggunganPekerja, int>, SuTanggunganPekerjaRepository>();
            services.AddTransient<IRepository<JAgama, int, string>, JAgamaRepository>();
            services.AddTransient<IRepository<JBangsa, int, string>, JBangsaRepository>();
            services.AddTransient<IRepository<JCaraBayar, int, string>, JCaraBayarRepository>();
            services.AddTransient<IRepository<AbBukuVot, int, string>, AbBukuVotRepository>();
            services.AddTransient<IRepository<JJantina, int, string>, JJantinaRepository>();
            services.AddTransient<IRepository<JBahagian, int, string>, JBahagianRepository>();
            services.AddTransient<IRepository<JPelulus, int, string>, JPelulusRepository>();
            services.AddTransient<IRepository<JPenyemak, int, string>, JPenyemakRepository>();
            services.AddTransient<IRepository<AkPenghutang, int, string>, AkPenghutangRepository>();
            services.AddTransient<IRepository<JSuGredGaji, int, string>, JSuGredGajiRepository>();
            services.AddTransient<IRepository<JSuTarafJawatan, int, string>, JSuTarafJawatanRepository>();
            services.AddTransient<IRepository<JSuKodGaji, int, string>, JSuKodGajiRepository>();

            //TUNAI RUNCIT
            services.AddTransient<IRepository<AkTunaiRuncit, int, string>, AkTunaiRuncitRepository>();
            services.AddTransient<IRepository<AkTunaiCV, int, string>, AkTunaiCVRepository>();
            services.AddTransient<IRepository<AkTunaiLejar, int, string>, AkTunaiLejarRepository>();
            //TUNAI RUNCIT END

            //PENDAHULUAN PELBAGAI
            services.AddTransient<IRepository<JTahapAktiviti, int, string>, JTahapAktivitiRepository>();
            services.AddTransient<IRepository<JSukan, int, string>, JSukanRepository>();
            services.AddTransient<IRepository<SpPendahuluanPelbagai, int, string>, SpPendahuluanPelbagaiRepository>();
            services.AddTransient<ListViewIRepository<SpPendahuluanPelbagai1, int>, SpPendahuluanPelbagai1Repository>();
            services.AddTransient<ListViewIRepository<SpPendahuluanPelbagai2, int>, SpPendahuluanPelbagai2Repository>();
            //PENDAHULUAN PELBAGAI END

            //SKIM KECEMERLANGAN ATLET DAN ELAUN JURURULATIH
            services.AddTransient<IRepository<SuAtlet, int, string>, SuAtletRepository>();
            services.AddTransient<IRepository<SuJurulatih, int, string>, SuJurulatihRepository>();
            services.AddTransient<IRepository<SuProfil, int, string>, SuProfilAtletRepository>();
            services.AddTransient<IRepository<SuProfil, int, string>, SuProfilJurulatihRepository>();
            services.AddTransient<IRepository<SuProfil1, int, string>, SuProfil1Repository>();
            //SKIM KECEMERLANGAN ATLET DAN ELAUN JURURULATIH END
            services.AddTransient<IRepository<AkNotaMinta, int, string>, AkNotaMintaRepository>();
            services.AddTransient<ListViewIRepository<AkNotaMinta1, int>, AkNotaMinta1Repository>();
            services.AddTransient<ListViewIRepository<AkNotaMinta2, int>, AkNotaMinta2Repository>();

            //BIZ CHANNEL
            services.AddTransient<IRepository<AkCimbEFT, int, string>, AkCimbEFTRepository>();
            services.AddTransient<ListViewIRepository<AkCimbEFT1, int>, AkCimbEFT1Repository>();
            //BIZ CHANNEl END

            //INVOIS DIKELUARKAN
            services.AddTransient<IRepository<AkInvois, int, string>, AkInvoisRepository>();
            services.AddTransient<ListViewIRepository<AkInvois1, int>, AkInvois1Repository>();
            services.AddTransient<ListViewIRepository<AkInvois2, int>, AkInvois2Repository>();
            //INVOIS DIKELUARKAN END

            services.AddTransient<IRepository<AbWaran, int, string>, AbWaranRepository>();
            services.AddTransient<ListViewIRepository<AbWaran1, int>, AbWaran1Repository>();

            //PENYATA PEMUNGUT
            services.AddTransient<IRepository<AkPenyataPemungut, int, string>, AkPenyataPemungutRepository>();
            services.AddTransient<ListViewIRepository<AkPenyataPemungut1, int>, AkPenyataPemungut1Repository>();
            services.AddTransient<ListViewIRepository<AkPenyataPemungut2, int>, AkPenyataPemungut2Repository>();
            //PENYATA PEMUNGUT END

            services.AddTransient<CustomIRepository<string, int>, CustomRepository>();
            services.AddTransient<BelanjawanSemasaIRepository<string, int>, BelanjawanSemasaRepository>();

            // BANK RECON
            services.AddTransient<IRepository<AkBankRecon, int, string>, AkBankReconRepository>();
            services.AddTransient<ListViewIRepository<AkBankReconPenyataBank, int>, AkBankReconPenyataBankRepository>();
            // BANK RECON END
            services.AddScoped(ss => SessionCartTerima.GetCart(ss));
            services.AddScoped(ss => SessionCartPendahuluan.GetCart(ss));
            services.AddScoped(ss => SessionCartPO.GetCart(ss));
            services.AddScoped(ss => SessionCartInden.GetCart(ss));
            services.AddScoped(ss => SessionCartPOLaras.GetCart(ss));
            services.AddScoped(ss => SessionCartJurnal.GetCart(ss));
            services.AddScoped(ss => SessionCartBelian.GetCart(ss));
            services.AddScoped(ss => SessionCartPV.GetCart(ss));
            services.AddScoped(ss => SessionCartPekerja.GetCart(ss));
            services.AddScoped(ss => SessionCartTunaiRuncit.GetCart(ss));
            services.AddScoped(ss => SessionCartTunaiCV.GetCart(ss));
            services.AddScoped(ss => SessionCartNotaMinta.GetCart(ss));
            services.AddScoped(ss => SessionCartWaran.GetCart(ss));
            services.AddScoped(ss => SessionCartAtlet.GetCart(ss));
            services.AddScoped(ss => SessionCartJurulatih.GetCart(ss));
            services.AddScoped(ss => SessionCartCimbEFT.GetCart(ss));
            services.AddScoped(ss => SessionCartInvois.GetCart(ss));
            services.AddScoped(ss => SessionCartPenyataPemungut.GetCart(ss));
            services.AddScoped(ss => SessionCartBankRecon.GetCart(ss));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAuthorization(options=>
            {
                //Menu Terimaan
                //Resit Rasmi
                options.AddPolicy("PR001", policy => policy.RequireClaim("PR001"));
                options.AddPolicy("PR001C", policy => policy.RequireClaim("PR001C"));
                options.AddPolicy("PR001E", policy => policy.RequireClaim("PR001E"));
                options.AddPolicy("PR001D", policy => policy.RequireClaim("PR001D"));
                options.AddPolicy("PR001P", policy => policy.RequireClaim("PR001P"));
                options.AddPolicy("PR001B", policy => policy.RequireClaim("PR001B"));
                options.AddPolicy("PR001R", policy => policy.RequireClaim("PR001R"));
                options.AddPolicy("PR001T", policy => policy.RequireClaim("PR001T"));
                options.AddPolicy("PR001UT", policy => policy.RequireClaim("PR001UT"));
                //Resit Rasmi end
                //Penyata Pemungut
                options.AddPolicy("PR002", policy => policy.RequireClaim("PR002"));
                options.AddPolicy("PR002C", policy => policy.RequireClaim("PR002C"));
                options.AddPolicy("PR002E", policy => policy.RequireClaim("PR002E"));
                options.AddPolicy("PR002D", policy => policy.RequireClaim("PR002D"));
                options.AddPolicy("PR002P", policy => policy.RequireClaim("PR002P"));
                options.AddPolicy("PR002B", policy => policy.RequireClaim("PR002B"));
                options.AddPolicy("PR002R", policy => policy.RequireClaim("PR002R"));
                options.AddPolicy("PR002T", policy => policy.RequireClaim("PR002T"));
                options.AddPolicy("PR002UT", policy => policy.RequireClaim("PR002UT"));
                //Penyata Pemungut end
                //Menu Tanggungan
                //Pesanan Tempatan
                options.AddPolicy("TG001", policy => policy.RequireClaim("TG001"));
                options.AddPolicy("TG001C", policy => policy.RequireClaim("TG001C"));
                options.AddPolicy("TG001E", policy => policy.RequireClaim("TG001E"));
                options.AddPolicy("TG001D", policy => policy.RequireClaim("TG001D"));
                options.AddPolicy("TG001P", policy => policy.RequireClaim("TG001P"));
                options.AddPolicy("TG001B", policy => policy.RequireClaim("TG001B"));
                options.AddPolicy("TG001R", policy => policy.RequireClaim("TG001R"));
                options.AddPolicy("TG001T", policy => policy.RequireClaim("TG001T"));
                options.AddPolicy("TG001UT", policy => policy.RequireClaim("TG001UT"));
                //Pesanan Tempatan End
                //Inden
                options.AddPolicy("TG003", policy => policy.RequireClaim("TG003"));
                options.AddPolicy("TG003C", policy => policy.RequireClaim("TG003C"));
                options.AddPolicy("TG003E", policy => policy.RequireClaim("TG003E"));
                options.AddPolicy("TG003D", policy => policy.RequireClaim("TG003D"));
                options.AddPolicy("TG003P", policy => policy.RequireClaim("TG003P"));
                options.AddPolicy("TG003B", policy => policy.RequireClaim("TG003B"));
                options.AddPolicy("TG003R", policy => policy.RequireClaim("TG003R"));
                options.AddPolicy("TG003T", policy => policy.RequireClaim("TG003T"));
                options.AddPolicy("TG003UT", policy => policy.RequireClaim("TG003UT"));
                //Inden End
                //Pelarasan PO
                options.AddPolicy("PT001", policy => policy.RequireClaim("PT001"));
                options.AddPolicy("PT001C", policy => policy.RequireClaim("PT001C"));
                options.AddPolicy("PT001E", policy => policy.RequireClaim("PT001E"));
                options.AddPolicy("PT001D", policy => policy.RequireClaim("PT001D"));
                options.AddPolicy("PT001P", policy => policy.RequireClaim("PT001P"));
                options.AddPolicy("PT001B", policy => policy.RequireClaim("PT001B"));
                options.AddPolicy("PT001R", policy => policy.RequireClaim("PT001R"));
                options.AddPolicy("PT001T", policy => policy.RequireClaim("PT001T"));
                options.AddPolicy("PT001UT", policy => policy.RequireClaim("PT001UT"));
                //Pesanan PO End
                //Invois Pembekal
                options.AddPolicy("TG002", policy => policy.RequireClaim("TG002"));
                options.AddPolicy("TG002C", policy => policy.RequireClaim("TG002C"));
                options.AddPolicy("TG002E", policy => policy.RequireClaim("TG002E"));
                options.AddPolicy("TG002D", policy => policy.RequireClaim("TG002D"));
                //options.AddPolicy("TG002P", policy => policy.RequireClaim("TG002P"));
                options.AddPolicy("TG002B", policy => policy.RequireClaim("TG002B"));
                options.AddPolicy("TG002R", policy => policy.RequireClaim("TG002R"));
                options.AddPolicy("TG002T", policy => policy.RequireClaim("TG002T"));
                options.AddPolicy("TG002UT", policy => policy.RequireClaim("TG002UT"));
                //Invois Pembekal End
                //Menu Baucer
                //Baucer Pembayaran
                options.AddPolicy("PV001", policy => policy.RequireClaim("PV001"));
                options.AddPolicy("PV001C", policy => policy.RequireClaim("PV001C"));
                options.AddPolicy("PV001E", policy => policy.RequireClaim("PV001E"));
                options.AddPolicy("PV001D", policy => policy.RequireClaim("PV001D"));
                options.AddPolicy("PV001P", policy => policy.RequireClaim("PV001P"));
                options.AddPolicy("PV001B", policy => policy.RequireClaim("PV001B"));
                options.AddPolicy("PV001R", policy => policy.RequireClaim("PV001R"));
                options.AddPolicy("PV001T", policy => policy.RequireClaim("PV001T"));
                options.AddPolicy("PV001UT", policy => policy.RequireClaim("PV001UT"));
                //Baucer Pembayaran End
                //Baucer Jurnal
                options.AddPolicy("JU001", policy => policy.RequireClaim("JU001"));
                options.AddPolicy("JU001C", policy => policy.RequireClaim("JU001C"));
                options.AddPolicy("JU001E", policy => policy.RequireClaim("JU001E"));
                options.AddPolicy("JU001D", policy => policy.RequireClaim("JU001D"));
                options.AddPolicy("JU001P", policy => policy.RequireClaim("JU001P"));
                options.AddPolicy("JU001B", policy => policy.RequireClaim("JU001B"));
                options.AddPolicy("JU001R", policy => policy.RequireClaim("JU001R"));
                options.AddPolicy("JU001T", policy => policy.RequireClaim("JU001T"));
                options.AddPolicy("JU001UT", policy => policy.RequireClaim("JU001UT"));
                //Baucer Jurnal End
                //Menu Tunai Runcit
                //Tunai Keluar
                options.AddPolicy("TR001", policy => policy.RequireClaim("TR001"));
                options.AddPolicy("TR001C", policy => policy.RequireClaim("TR001C"));
                options.AddPolicy("TR001E", policy => policy.RequireClaim("TR001E"));
                options.AddPolicy("TR001D", policy => policy.RequireClaim("TR001D"));
                options.AddPolicy("TR001P", policy => policy.RequireClaim("TR001P"));
                options.AddPolicy("TR001B", policy => policy.RequireClaim("TR001B"));
                options.AddPolicy("TR001R", policy => policy.RequireClaim("TR001R"));
                options.AddPolicy("TR001T", policy => policy.RequireClaim("TR001T"));
                options.AddPolicy("TR001UT", policy => policy.RequireClaim("TR001UT"));
                //Tunai Keluar End
                //Menu Nota Minta
                //Nota Minta
                options.AddPolicy("NM001", policy => policy.RequireClaim("NM001"));
                options.AddPolicy("NM001C", policy => policy.RequireClaim("NM001C"));
                options.AddPolicy("NM001E", policy => policy.RequireClaim("NM001E"));
                options.AddPolicy("NM001E1", policy => policy.RequireClaim("NM001E1"));
                options.AddPolicy("NM001D", policy => policy.RequireClaim("NM001D"));
                options.AddPolicy("NM001P", policy => policy.RequireClaim("NM001P"));
                options.AddPolicy("NM001B", policy => policy.RequireClaim("NM001B"));
                options.AddPolicy("NM001R", policy => policy.RequireClaim("NM001R"));
                options.AddPolicy("NM001T", policy => policy.RequireClaim("NM001T"));
                options.AddPolicy("NM001UT", policy => policy.RequireClaim("NM001UT"));
                //Nota Minta End
                //Menu Permohonan
                //Pendahuluan Pelbagai
                options.AddPolicy("SP001", policy => policy.RequireClaim("SP001"));
                options.AddPolicy("SP001C", policy => policy.RequireClaim("SP001C"));
                options.AddPolicy("SP001E", policy => policy.RequireClaim("SP001E"));
                options.AddPolicy("SP001D", policy => policy.RequireClaim("SP001D"));
                options.AddPolicy("SP001P", policy => policy.RequireClaim("SP001P"));
                options.AddPolicy("SP001B", policy => policy.RequireClaim("SP001B"));
                options.AddPolicy("SP001R", policy => policy.RequireClaim("SP001R"));
                options.AddPolicy("SP001T", policy => policy.RequireClaim("SP001T"));
                options.AddPolicy("SP001UT", policy => policy.RequireClaim("SP001UT"));
                //Pendahuluan Pelbagai End
                //Menu Belanjawan
                //Waran
                options.AddPolicy("BJ001", policy => policy.RequireClaim("BJ001"));
                options.AddPolicy("BJ001C", policy => policy.RequireClaim("BJ001C"));
                options.AddPolicy("BJ001E", policy => policy.RequireClaim("BJ001E"));
                options.AddPolicy("BJ001D", policy => policy.RequireClaim("BJ001D"));
                options.AddPolicy("BJ001P", policy => policy.RequireClaim("BJ001P"));
                options.AddPolicy("BJ001B", policy => policy.RequireClaim("BJ001B"));
                options.AddPolicy("BJ001R", policy => policy.RequireClaim("BJ001R"));
                options.AddPolicy("BJ001T", policy => policy.RequireClaim("BJ001T"));
                options.AddPolicy("BJ001UT", policy => policy.RequireClaim("BJ001UT"));
                //Waran End
                //Belanjawan Semasa
                options.AddPolicy("BJ002", policy => policy.RequireClaim("BJ002"));
                options.AddPolicy("BJ002P", policy => policy.RequireClaim("BJ002P"));
                //Belanjawan Semasa End
                //Menu Profil
                //Profil Atlet
                options.AddPolicy("SU001", policy => policy.RequireClaim("SU001"));
                options.AddPolicy("SU001C", policy => policy.RequireClaim("SU001C"));
                options.AddPolicy("SU001E", policy => policy.RequireClaim("SU001E"));
                options.AddPolicy("SU001D", policy => policy.RequireClaim("SU001D"));
                options.AddPolicy("SU001P", policy => policy.RequireClaim("SU001P"));
                options.AddPolicy("SU001B", policy => policy.RequireClaim("SU001B"));
                options.AddPolicy("SU001R", policy => policy.RequireClaim("SU001R"));
                options.AddPolicy("SU001T", policy => policy.RequireClaim("SU001T"));
                options.AddPolicy("SU001UT", policy => policy.RequireClaim("SU001UT"));
                //Profil Atlet End
                //Profil Jurulatih
                options.AddPolicy("SU002", policy => policy.RequireClaim("SU002"));
                options.AddPolicy("SU002C", policy => policy.RequireClaim("SU002C"));
                options.AddPolicy("SU002E", policy => policy.RequireClaim("SU002E"));
                options.AddPolicy("SU002D", policy => policy.RequireClaim("SU002D"));
                options.AddPolicy("SU002P", policy => policy.RequireClaim("SU002P"));
                options.AddPolicy("SU002B", policy => policy.RequireClaim("SU002B"));
                options.AddPolicy("SU002R", policy => policy.RequireClaim("SU002R"));
                options.AddPolicy("SU002T", policy => policy.RequireClaim("SU002T"));
                options.AddPolicy("SU002UT", policy => policy.RequireClaim("SU002UT"));
                //Profil Jurulatih End
                //Menu EFT
                //Biz Channel
                options.AddPolicy("PV002", policy => policy.RequireClaim("PV002"));
                options.AddPolicy("PV002C", policy => policy.RequireClaim("PV002C"));
                options.AddPolicy("PV002E", policy => policy.RequireClaim("PV002E"));
                options.AddPolicy("PV002D", policy => policy.RequireClaim("PV002D"));
                options.AddPolicy("PV002P", policy => policy.RequireClaim("PV002P"));
                options.AddPolicy("PV002B", policy => policy.RequireClaim("PV002B"));
                options.AddPolicy("PV002R", policy => policy.RequireClaim("PV002R"));
                //Biz Channel End
                //Menu Invois
                //Invois Dikeluarkan
                options.AddPolicy("IN001", policy => policy.RequireClaim("IN001"));
                options.AddPolicy("IN001C", policy => policy.RequireClaim("IN001C"));
                options.AddPolicy("IN001E", policy => policy.RequireClaim("IN001E"));
                options.AddPolicy("IN001D", policy => policy.RequireClaim("IN001D"));
                options.AddPolicy("IN001P", policy => policy.RequireClaim("IN001P"));
                options.AddPolicy("IN001B", policy => policy.RequireClaim("IN001B"));
                options.AddPolicy("IN001R", policy => policy.RequireClaim("IN001R"));
                options.AddPolicy("IN001T", policy => policy.RequireClaim("IN001T"));
                options.AddPolicy("IN001UT", policy => policy.RequireClaim("IN001UT"));
                //Invois Dikeluarkan End
                //Menu Daftar
                //Anggota
                options.AddPolicy("DF001", policy => policy.RequireClaim("DF001"));
                options.AddPolicy("DF001C", policy => policy.RequireClaim("DF001C"));
                options.AddPolicy("DF001E", policy => policy.RequireClaim("DF001E"));
                options.AddPolicy("DF001D", policy => policy.RequireClaim("DF001D"));
                options.AddPolicy("DF001B", policy => policy.RequireClaim("DF001B"));
                options.AddPolicy("DF001R", policy => policy.RequireClaim("DF001R"));
                //Anggota End
                //Pembekal
                options.AddPolicy("DF002", policy => policy.RequireClaim("DF002"));
                options.AddPolicy("DF002C", policy => policy.RequireClaim("DF002C"));
                options.AddPolicy("DF002E", policy => policy.RequireClaim("DF002E"));
                options.AddPolicy("DF002D", policy => policy.RequireClaim("DF002D"));
                options.AddPolicy("DF002B", policy => policy.RequireClaim("DF002B"));
                options.AddPolicy("DF002R", policy => policy.RequireClaim("DF002R"));
                //Pembekal End
                //Penghutang
                options.AddPolicy("DF003", policy => policy.RequireClaim("DF003"));
                options.AddPolicy("DF003C", policy => policy.RequireClaim("DF003C"));
                options.AddPolicy("DF003E", policy => policy.RequireClaim("DF003E"));
                options.AddPolicy("DF003D", policy => policy.RequireClaim("DF003D"));
                options.AddPolicy("DF003B", policy => policy.RequireClaim("DF003B"));
                options.AddPolicy("DF003R", policy => policy.RequireClaim("DF003R"));
                //Penghutang End
                //P. Tunai Runcit
                options.AddPolicy("DF004", policy => policy.RequireClaim("DF004"));
                options.AddPolicy("DF004C", policy => policy.RequireClaim("DF004C"));
                options.AddPolicy("DF004E", policy => policy.RequireClaim("DF004E"));
                options.AddPolicy("DF004D", policy => policy.RequireClaim("DF004D"));
                options.AddPolicy("DF004P", policy => policy.RequireClaim("DF004P"));
                options.AddPolicy("DF004R", policy => policy.RequireClaim("DF004R"));
                options.AddPolicy("DF004T", policy => policy.RequireClaim("DF004T"));
                //P. Tunai Runcit End
                //Atlet
                options.AddPolicy("DF005", policy => policy.RequireClaim("DF005"));
                options.AddPolicy("DF005C", policy => policy.RequireClaim("DF005C"));
                options.AddPolicy("DF005E", policy => policy.RequireClaim("DF005E"));
                options.AddPolicy("DF005D", policy => policy.RequireClaim("DF005D"));
                options.AddPolicy("DF005B", policy => policy.RequireClaim("DF005B"));
                options.AddPolicy("DF005R", policy => policy.RequireClaim("DF005R"));
                //Atlet End
                //Jurulatih
                options.AddPolicy("DF006", policy => policy.RequireClaim("DF006"));
                options.AddPolicy("DF006C", policy => policy.RequireClaim("DF006C"));
                options.AddPolicy("DF006E", policy => policy.RequireClaim("DF006E"));
                options.AddPolicy("DF006D", policy => policy.RequireClaim("DF006D"));
                options.AddPolicy("DF006B", policy => policy.RequireClaim("DF006B"));
                options.AddPolicy("DF006R", policy => policy.RequireClaim("DF006R"));
                //Jurulatih End
                //Laporan
                options.AddPolicy("LP001", policy => policy.RequireClaim("LP001"));
                //Laporan End
                //Menu Penyesuaian Bank
                //Penyesuaian Bank
                options.AddPolicy("PB001", policy => policy.RequireClaim("PB001"));
                options.AddPolicy("PB001C", policy => policy.RequireClaim("PB001C"));
                options.AddPolicy("PB001E", policy => policy.RequireClaim("PB001E"));
                options.AddPolicy("PB001D", policy => policy.RequireClaim("PB001D"));
                options.AddPolicy("PB001P", policy => policy.RequireClaim("PB001P"));
                options.AddPolicy("PB001R", policy => policy.RequireClaim("PB001R"));
                //Penyesuaian Bank end
            });

            services.AddMvc(f =>
            {
                f.OutputFormatters.RemoveType
                (typeof(HttpNoContentOutputFormatter));
                f.OutputFormatters.Insert(0, new
                HttpNoContentOutputFormatter
                {
                    TreatNullValueAsNoContent = false
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<UnhandledExceptionFilterAttribute>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            // debug shown on development
            //app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/Home/Error");
            //app.UseHsts();
            app.UseDeveloperExceptionPage();
            // debug shown on development end

            // development shown normally
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            // end
            app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

            // redirect 404 page not found
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 400)
            //    {
            //        context.Request.Path = "/Home/GlobalError";
            //        await next();
            //    }
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            //var contentRootPath = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");
            //var webRootPath = (string)AppDomain.CurrentDomain.GetData("WebRootPath");

            //// setup app's root folders
            //AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
            //AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.SeedUsers(userManager);
            RotativaConfiguration.Setup(env.ContentRootPath, "wwwroot/plugins/Rotativa");

        }
    }
}
