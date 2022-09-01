using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SUMBER.Data;
using SUMBER.Models.Administration;
using System;
using System.Linq;

namespace SUMBER.Models.Modules
{
    public static class SeedData
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("superadmin@idwal.com.my").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "superadmin@idwal.com.my",
                    Email = "superadmin@idwal.com.my",
                    Nama = "SuperAdmin"
                };

                IdentityResult result = userManager.CreateAsync(user, "SuperIdwalsys57#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                } 
            }
            else
            {
                userManager.AddToRoleAsync(userManager.FindByEmailAsync("superadmin@idwal.com.my").Result, "SuperAdmin").Wait();
            }
        }

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.SiAppInfo.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                CompanyDetails company = new CompanyDetails();

                context.SiAppInfo.AddRange(
                    new SiAppInfo
                    {
                        KodSistem = company.KodSistem,
                        TarVersi = DateTime.Today,
                        NamaSyarikat = company.NamaSyarikat,
                        NoPendaftaran = company.NoPendaftaran,
                        AlamatSyarikat1 = company.AlamatSyarikat1,
                        AlamatSyarikat2 = company.AlamatSyarikat2,
                        AlamatSyarikat3 = company.AlamatSyarikat3,
                        Bandar = company.Bandar,
                        Poskod = company.Poskod,
                        Daerah = company.Daerah,
                        Negeri = company.Negeri,
                        TelSyarikat = company.TelSyarikat,
                        FaksSyarikat = company.FaksSyarikat,
                        EmelSyarikat = company.EmelSyarikat,
                        TarMula = DateTime.Today,
                        CompanyLogoWeb = company.CompanyLogoWeb,
                        CompanyLogoPrintPDF = company.CompanyLogoPrintPDF

            }
                );
            }

            //// Look for any movies.
            if (context.JKW.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                context.JKW.AddRange(
                    new JKW
                    {
                        Kod = "100",
                        Perihal = "MAJLIS SUKAN NEGERI KEDAH"
                    }
                );
            }

            //if (context.JCaraBayar.Any())
            //{
            //    //return;
            //}
            //else
            //{
            //    context.JCaraBayar.AddRange(
            //        new JCaraBayar
            //        {
            //            Kod = "TN",
            //            Perihal = "TUNAI"
            //        },
            //        new JCaraBayar
            //        {
            //            Kod = "CK",
            //            Perihal = "CEK"
            //        },
            //        new JCaraBayar
            //        {
            //            Kod = "MK",
            //            Perihal = "MAKLUMAN KREDIT"
            //        },
            //        new JCaraBayar
            //        {
            //            Kod = "EF",
            //            Perihal = "EFT"
            //        },
            //        new JCaraBayar
            //        {
            //            Kod = "FP",
            //            Perihal = "FPX"
            //        }
            //    );
            //}
            if (context.Roles.Any())
            {
                //context.Roles.AddRange(
                //    new IdentityRole { Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() }
                //    );
            }
            else
            {
                context.Roles.AddRange(
                    new IdentityRole { Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() },
                   new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() },
                    new IdentityRole { Name = "Supervisor", NormalizedName = "Supervisor".ToUpper() },
                    new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() }
                    );
            }

            if (context.SiModul.Any())
            {
                //return;
            }
            else
            {
                context.SiModul.AddRange(
                    new SiModul
                    {
                        FuncId = "SY001",
                        FuncName = "Maklumat Pengguna",
                        Model = "User"
                    },
                    new SiModul
                    {
                        FuncId = "SY002",
                        FuncName = "Maklumat Tahap Pengguna",
                        Model = "Roles"
                    },
                    new SiModul
                    {
                        FuncId = "SY003",
                        FuncName = "Log Transaksi Pengguna",
                        Model = "AppLog"
                    },
                    new SiModul
                    {
                        FuncId = "SY004",
                        FuncName = "Info Syarikat",
                        Model = "SiAppInfo"
                    },
                    new SiModul
                    {
                        FuncId = "JD006",
                        FuncName = "Jadual Kumpulan Wang",
                        Model = "JKW"
                    },
                    new SiModul
                    {
                        FuncId = "JD002",
                        FuncName = "Jadual Bahagian",
                        Model = "JBahagian"
                    },
                    new SiModul
                    {
                        FuncId = "JD009",
                        FuncName = "Jadual Akaun Bank",
                        Model = "AkBank"
                    },
                    new SiModul
                    {
                        FuncId = "JD004",
                        FuncName = "Jadual Bank",
                        Model = "JBank"
                    },
                    new SiModul
                    {
                        FuncId = "JD005",
                        FuncName = "Jadual Cara Bayar",
                        Model = "JCaraBayar"
                    },
                    new SiModul
                    {
                        FuncId = "JD001",
                        FuncName = "Jadual Agama",
                        Model = "JAgama"
                    },
                    new SiModul
                    {
                        FuncId = "JD003",
                        FuncName = "Jadual Bangsa",
                        Model = "JBangsa"
                    },
                    new SiModul
                    {
                        FuncId = "JD008",
                        FuncName = "Jadual Sukan",
                        Model = "JSukan"
                    },
                    new SiModul
                    {
                        FuncId = "JD013",
                        FuncName = "Jadual Tahap Aktiviti",
                        Model = "JTahapAktiviti"
                    },
                    new SiModul
                    {
                        FuncId = "JD007",
                        FuncName = "Jadual Negeri",
                        Model = "JNegeri"
                    },
                    new SiModul
                    {
                        FuncId = "JD011",
                        FuncName = "Jadual Penyemak",
                        Model = "JPenyemak"
                    },
                    new SiModul
                    {
                        FuncId = "JD010",
                        FuncName = "Jadual Pelulus",
                         Model = "JPelulus"
                    },
                    new SiModul
                    {
                        FuncId = "JD012",
                        FuncName = "Jadual Profil Kategori",
                        Model = "JProfilKategori"
                    },
                    new SiModul
                    {
                        FuncId = "DF001",
                        FuncName = "Daftar Anggota",
                        Model = "SuPekerja"
                    },
                    new SiModul
                    {
                        FuncId = "DF002",
                        FuncName = "Daftar Pembekal",
                        Model = "AkPembekal"
                    },
                    new SiModul
                    {
                        FuncId = "DF003",
                        FuncName = "Daftar Penghutang",
                        Model = "AkPenghutang"
                    },
                    new SiModul
                    {
                        FuncId = "DF004",
                        FuncName = "Daftar P. Tunai Runcit",
                        Model = "AkTunaiRuncit",
                        FuncIdOld = "TR001"
                    },
                    new SiModul
                    {
                        FuncId = "DF005",
                        FuncName = "Daftar Atlet",
                        Model = "SuAtlet"
                    },
                    new SiModul
                    {
                        FuncId = "DF006",
                        FuncName = "Daftar Jurulatih",
                        Model = "SuJurulatih"
                    },
                    new SiModul
                    {
                        FuncId = "BJ001",
                        FuncName = "Belanjawan Waran",
                        Model = "AbWaran"
                    },
                    new SiModul
                    {
                        FuncId = "AK001",
                        FuncName = "Carta Akaun",
                        Model = "AkCarta"
                    },
                    new SiModul
                    {
                        FuncId = "AK002",
                        FuncName = "Lejar Am",
                        Model = "AkAkaun"
                    },
                    new SiModul
                    {
                        FuncId = "AK003",
                        FuncName = "Buku Vot",
                        Model = "AbBukuVot"
                    },
                    new SiModul
                    {
                        FuncId = "SU001",
                        FuncName = "Profil Atlet",
                        Model = "SuProfil"
                    },
                    new SiModul
                    {
                        FuncId = "SU002",
                        FuncName = "Profil Jurulatih",
                        Model ="SuProfil"
                    },
                    new SiModul
                    {
                        FuncId = "SP001",
                        FuncName = "Pendahuluan Pelbagai",
                        Model = "SpPendahuluanPelbagai"
                    },
                    new SiModul
                    {
                        FuncId = "IN001",
                        FuncName = "Invois Dikeluarkan",
                        Model = "AkInvois"
                    },
                    new SiModul
                    {
                        FuncId = "PR001",
                        FuncName = "Penerimaan",
                        Model = "AkTerima"
                    },
                    new SiModul
                    {
                        FuncId = "PR002",
                        FuncName = "Penyata Pemungut",
                        Model = "AkPenyataPemungut"
                    },
                    new SiModul
                    {
                        FuncId = "NM001",
                        FuncName = "Nota Minta",
                        Model = "AkNotaMinta"
                    },
                    new SiModul
                    {
                        FuncId = "TG001",
                        FuncName = "Pesanan Tempatan",
                        Model = "AkPO"
                    },
                    new SiModul
                    {
                        FuncId = "TG003",
                        FuncName = "Inden Kerja",
                        Model = "AkInden"
                    },
                    new SiModul
                    {
                        FuncId = "PT001",
                        FuncName = "Pelarasan Pesanan Tempatan",
                        Model = "AkPOLaras"
                    },
                    new SiModul
                    {
                        FuncId = "TG002",
                        FuncName = "Invois Pembekal",
                        Model = "AkBelian"
                    },
                    new SiModul
                    {
                        FuncId = "PV001",
                        FuncName = "Baucer Pembayaran",
                        Model = "AkPV"
                    },
                    new SiModul
                    {
                        FuncId = "PV002",
                        FuncName = "Biz Channel",
                        Model = "AkCimbEFT"
                    },
                    new SiModul
                    {
                        FuncId = "JU001",
                        FuncName = "Baucer Jurnal",
                        Model = "AkJurnal"
                    },
                    new SiModul
                    {
                        FuncId = "TR001",
                        FuncName = "Tunai Keluar",
                        Model = "AkTunaiCV",
                        FuncIdOld = "TR002"
                    },
                    new SiModul
                    {
                        FuncId = "LPN001",
                        FuncName = "Laporan Daftar Bil / Nota Minta"
                    },
                    new SiModul
                    {
                        FuncId = "LPV001",
                        FuncName = "Laporan Daftar Baucer"
                    },
                    new SiModul
                    {
                        FuncId = "LPR001",
                        FuncName = "Laporan Daftar Resit"
                    }
                );
            }

            if (context.JJantina.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                context.JJantina.AddRange(
                    new JJantina
                    {
                        Perihal = "LELAKI"
                    },
                    new JJantina
                    {
                        Perihal = "PEREMPUAN"
                    }
                );
            }

            //if (context.JBank.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    context.JBank.AddRange(
            //        new JBank
            //        {
            //            Kod = "BIMB",
            //            Nama = "BANK ISLAM MALAYSIA BERHAD"
            //        },
            //        new JBank
            //        {
            //            Kod = "BMMB",
            //            Nama = "BANK MUAMALAT MALAYSIA BERHAD"
            //        },
            //        new JBank
            //        {
            //            Kod = "MBB",
            //            Nama = "MALAYAN BANKING BERHAD"
            //        }
            //    );
            //}

            if (context.JNegeri.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                context.JNegeri.AddRange(
                    new JNegeri
                    {
                        Kod = "01",
                        Perihal = "JOHOR"
                    },
                    new JNegeri
                    {
                        Kod = "02",
                        Perihal = "KEDAH"
                    },
                    new JNegeri
                    {
                        Kod = "03",
                        Perihal = "KELANTAN"
                    },
                    new JNegeri
                    {
                        Kod = "04",
                        Perihal = "MELAKA"
                    },
                    new JNegeri
                    {
                        Kod = "05",
                        Perihal = "NEGERI SEMBILAN"
                    },
                    new JNegeri
                    {
                        Kod = "06",
                        Perihal = "PAHANG"
                    },
                    new JNegeri
                    {
                        Kod = "07",
                        Perihal = "PULAU PINANG"
                    },
                    new JNegeri
                    {
                        Kod = "08",
                        Perihal = "PERAK"
                    },
                    new JNegeri
                    {
                        Kod = "09",
                        Perihal = "PERLIS"
                    },
                    new JNegeri
                    {
                        Kod = "10",
                        Perihal = "SELANGOR"
                    },
                    new JNegeri
                    {
                        Kod = "11",
                        Perihal = "TERENGGANU"
                    },
                    new JNegeri
                    {
                        Kod = "12",
                        Perihal = "SABAH"
                    },
                    new JNegeri
                    {
                        Kod = "13",
                        Perihal = "SARAWAK"
                    },
                    new JNegeri
                    {
                        Kod = "14",
                        Perihal = "WILAYAH PERSEKUTUAN (KUALA LUMPUR)"
                    },
                    new JNegeri
                    {
                        Kod = "15",
                        Perihal = "WILAYAH PERSEKUTUAN (LABUAN)"
                    },
                    new JNegeri
                    {
                        Kod = "16",
                        Perihal = "WILAYAH PERSEKUTUAN (PUTRAJAYA)"
                    }
                );
            }

            if (context.JJenis.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                context.JJenis.AddRange(
                    new JJenis
                    {
                        Kod = "L",
                        Nama = "LIABILITI"
                    },

                    new JJenis
                    {
                        Kod = "E",
                        Nama = "EKUITI"
                    },

                    new JJenis
                    {
                        Kod = "B",
                        Nama = "BELANJA"
                    },
                    new JJenis
                    {
                        Kod = "A",
                        Nama = "ASET"
                    },
                    new JJenis
                    {
                        Kod = "H",
                        Nama = "HASIL"
                    }

                );
            }

            if (context.JParas.Any())
            {
                //return;   // DB has been seeded
            }
            else
            {
                context.JParas.AddRange(
                    new JParas
                    {
                        Kod = "1"
                    },

                    new JParas
                    {
                        Kod = "2",
                    },

                    new JParas
                    {
                        Kod = "3"
                    },
                    new JParas
                    {
                        Kod = "4"
                    }

                );
            }

            //if (context.JAgama.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    context.JAgama.AddRange(
            //        new JAgama
            //        {
            //            Perihal = "ISLAM"
            //        },

            //        new JAgama
            //        {
            //            Perihal = "BUDHA"
            //        },

            //        new JAgama
            //        {
            //            Perihal = "KRISTIAN"
            //        },
            //        new JAgama
            //        {
            //            Perihal = "HINDU"
            //        },
            //        new JAgama
            //        {
            //            Perihal = "TIADA AGAMA"
            //        },
            //        new JAgama
            //        {
            //            Perihal = "LAIN-LAIN"
            //        }

            //    );
            //}

            //if (context.JBangsa.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    context.JBangsa.AddRange(
            //        new JBangsa
            //        {
            //            Perihal = "MELAYU",
            //        },

            //        new JBangsa
            //        {
            //            Perihal = "CINA"
            //        },

            //        new JBangsa
            //        {
            //            Perihal = "INDIA"
            //        },
            //        new JBangsa
            //        {
            //            Perihal = "LAIN-LAIN"
            //        }

            //    );
            //}

            //if (context.JJawatanPekerja.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    context.JJawatanPekerja.AddRange(
            //        new JJawatanPekerja
            //        {
            //            Kod = "JT",
            //            Perihal = "JURUTEKNIK"
            //        },

            //        new JJawatanPekerja
            //        {
            //            Kod = "PG",
            //            Perihal = "PEGAWAI"
            //        },

            //        new JJawatanPekerja
            //        {
            //            Kod = "PPG",
            //            Perihal = "PENOLONG PEGAWAI"
            //        }

            //    );
            //}
            context.SaveChanges();

            //Data with foreign key

            //if (context.AkCarta.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    var kw = context.JKW.Where(b => b.Kod == "100").FirstOrDefault();
            //    var jenisH = context.JJenis.Where(b => b.Kod == "H").FirstOrDefault();
            //    var jenisB = context.JJenis.Where(b => b.Kod == "H").FirstOrDefault();
            //    var jenisA = context.JJenis.Where(b => b.Kod == "A").FirstOrDefault();
            //    var paras1 = context.JParas.Where(b => b.Kod == "1").FirstOrDefault();
            //    var paras2 = context.JParas.Where(b => b.Kod == "2").FirstOrDefault();
            //    var paras3 = context.JParas.Where(b => b.Kod == "3").FirstOrDefault();
            //    var paras4 = context.JParas.Where(b => b.Kod == "4").FirstOrDefault();
            //    context.AkCarta.AddRange(
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A10000",
            //            Perihal = "ASET SEMASA",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras1.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A11000",
            //            Perihal = "WANG TUNAI DAN BAKI BANK",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras2.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A11100",
            //            Perihal = "WANG TUNAI DAN BAKI BANK",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras3.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A11101",
            //            Perihal = "BIMB ... ",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A11106",
            //            Perihal = "BMMB ... ",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "A11108",
            //            Perihal = "MBB ... ",
            //            JJenisId = jenisA.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H10000",
            //            Perihal = "Hasil Bukan Cukai",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras1.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11000",
            //            Perihal = "Hasil Bukan Cukai",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras2.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11100",
            //            Perihal = "Hasil Dokumen",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras3.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11101",
            //            Perihal = "Hasil Dokumen Tender",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11102",
            //            Perihal = "Hasil Dokumen Sebutharga",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11203",
            //            Perihal = "ZAKAT SIMPANAN",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11204",
            //            Perihal = "ZAKAT PENDAPATAN",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11206",
            //            Perihal = "ZAKAT PELABURAN/ SAHAM",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11207",
            //            Perihal = "ZAKAT KWSP DAN LTAT",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11219",
            //            Perihal = "ZAKAT HARTA ( FPX / EJEN PUNGUTAN )",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11220",
            //            Perihal = "ZAKAT EMAS / PERAK ( FPX / EJEN PUNGUTAN )",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11301",
            //            Perihal = "ZAKAT PERNIAGAAN",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "H11401",
            //            Perihal = "ZAKAT TANAMAN",
            //            JJenisId = jenisH.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "K",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        // AK CODE (23/12/2021)
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B20000",
            //            Perihal = "PERKHIDMATAN DAN BEKALAN",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras1.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23000",
            //            Perihal = "PERHUBUNGAN DAN UTILITI",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras2.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23100",
            //            Perihal = "PERHUBUNGAN",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras3.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23101",
            //            Perihal = "POS BIASA, MEL UDARA, MEL BERDAFTAR DAN EXPRESS",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23102",
            //            Perihal = "TELEFON TERMASUK SEWAAN DAN KOS PEMASANGAN ALAT",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23103",
            //            Perihal = "PERHUBUNGAN TELEKS, TELEGRAF DAN KABEL",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B23199",
            //            Perihal = "PERKHIDMATAN PERHUBUNGAN YANG LAIN",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "D",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B27000",
            //            Perihal = "BEKALAN DAN BAHAN-BAHAN LAIN",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras2.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B27100",
            //            Perihal = "BEKALAN PEJABAT",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras3.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B27101",
            //            Perihal = "BUKU, MAJALAH & AKHBAR",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B27102",
            //            Perihal = "ALAT TULIS KOMPUTER",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        },
            //        new AkCarta
            //        {
            //            JKWId = kw.Id,
            //            Kod = "B27199",
            //            Perihal = "BEKALAN-BEKALAN PEJABAT YANG LAIN",
            //            JJenisId = jenisB.Id,
            //            JParasId = paras4.Id,
            //            DebitKredit = "D",
            //            UmumDetail = "U",
            //            Catatan1 = "",
            //            Catatan2 = "",
            //            Baki = 0.00m
            //        }
            //        // AK CODE (23/12/2021) end
            //    );
            //}
            //context.SaveChanges();

            //if (context.AkBank.Any())
            //{
            //    //return;   // DB has been seeded
            //}
            //else
            //{
            //    var kw = context.JKW.Where(b => b.Kod == "100").FirstOrDefault();
            //    var bank = context.JBank.Where(b => b.Kod == "BIMB").FirstOrDefault();
            //    var carta = context.AkCarta.Where(b => b.Kod == "A11101").FirstOrDefault();

            //    context.AkBank.AddRange(
            //        new AkBank
            //        {
            //            JKWId = kw.Id,
            //            JBankId = 1,
            //            AkCartaId = carta.Id,
            //            Kod = "001",
            //            NoAkaun = "1200210005702"
            //        }
            //    );
            //}
            //context.SaveChanges();

            //if (context.AkPembekal.Any())
            //{
            //}
            //else
            //{
            //    var negeri = context.JNegeri.Where(b => b.Kod == "10").FirstOrDefault();
            //    var jbank = context.JBank.Where(b => b.Kod == "BIMB").FirstOrDefault();

            //    context.AkPembekal.AddRange(
            //        new AkPembekal
            //        {
            //            KodSykt = "I0001",
            //            NamaSykt = "IDWAL SYSTEMS SDN BHD",
            //            NoPendaftaran = "187842-T",
            //            Alamat1 = "LOT 605G, KOMPLEKS DIAMOND, BANGI BUSINESS PARK, ",
            //            Alamat2 = "JALAN MEDAN BANGI, OFF PERSIARAN BANDAR, ",
            //            Alamat3 = "",
            //            Poskod = "43650",
            //            Bandar = "BANDAR BARU BANGI ",
            //            JNegeriId = negeri.Id,
            //            Telefon1 = "+601133272978",
            //            Emel = "admin@idwal.com.my",
            //            AkaunBank = "1300882525",
            //            JBankId = jbank.Id
            //        }
            //    );
            //}
            //context.SaveChanges();

            //if (context.SuPekerja.Any())
            //{
            //}
            //else
            //{
            //    context.SuPekerja.AddRange(
            //        new SuPekerja
            //        {
            //            NoGaji = "A00001",
            //            Nama = "JAMES DOE",
            //            Alamat1 = "55 Jalan Raja Perempuan Mazwin Taman Rishah",
            //            Alamat2 = "",
            //            Poskod = "30100 ",
            //            Bandar = "",
            //            JNegeriId = context.JNegeri.Where(b => b.Perihal.Contains("PERAK")).FirstOrDefault().Id,
            //            TelefonBimbit = "0123456789",
            //            Emel = "jamesdoe@example.com",
            //            StatusKahwin = 0,
            //            BilAnak = 0,
            //            GajiPokok = 2000,
            //            TarikhMasukKerja = DateTime.Parse("2021-03-11"),
            //            JAgamaId = context.JAgama.Where(b => b.Perihal.Contains("LAIN")).FirstOrDefault().Id,
            //            JBangsaId = context.JBangsa.Where(b => b.Perihal.Contains("LAIN")).FirstOrDefault().Id,
            //            JJawatanPekerjaId = context.JJawatanPekerja.Where(b => b.Perihal.Contains("JURU")).FirstOrDefault().Id,
            //            JCaraBayarId = context.JCaraBayar.Where(b => b.Kod == "CK").FirstOrDefault().Id,
            //            NoAkaunBank = "1234509876"
            //        }
            //    );
            //}
            //context.SaveChanges();

            //if (context.AkAkaun.Any()) { }
            //else
            //{
            //    var kw = context.JKW.Where(b => b.Kod == "100").FirstOrDefault();
            //    var carta1 = context.AkCarta.Where(b => b.Kod == "A11108").FirstOrDefault();

            //    context.AkAkaun.AddRange(
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 00:19:32.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010001",
            //            Debit = 820,
            //            Kredit = 0
            //        }, 
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 00:19:32.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021011001",
            //            Debit = 835,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 00:24:37.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010002",
            //            Debit = 3629.10M,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 00:44:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010003",
            //            Debit = 2386.75m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 01:13:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010004",
            //            Debit = 303.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 01:33:07.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010005",
            //            Debit = 130.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 02:36:03.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010006",
            //            Debit = 1060.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 04:36:12.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11207").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010007",
            //            Debit = 150.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 04:54:47.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010008",
            //            Debit = 53.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:18:02.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010009",
            //            Debit = 800.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:38:33.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010010",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:39:17.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010011",
            //            Debit = 1600.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:42:05.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010012",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:43:50.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010013",
            //            Debit = 10.80m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:43:50.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021011013",
            //            Debit = 10.80m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 06:55:46.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010014",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:06:29.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010015",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:13:06.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010016",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:13:22.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010017",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:20:54.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010018",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:21:11.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010019",
            //            Debit = 74.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:24:11.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010020",
            //            Debit = 1000.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:29:06.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010021",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:30:06.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010022",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:30:06.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010022",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:41:14.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010023",
            //            Debit = 500.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:41:19.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11207").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010024",
            //            Debit = 25.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:47:27.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010025",
            //            Debit = 300.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:48:42.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11207").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010026",
            //            Debit = 25.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:52:34.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11207").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010027",
            //            Debit = 25.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:55:19.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010028",
            //            Debit = 900.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 07:56:09.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11207").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010029",
            //            Debit = 25.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 08:28:57.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010030",
            //            Debit = 954.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:28:29.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021010031",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:31:05.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000032",
            //            Debit = 500.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:32:03.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000033",
            //            Debit = 1250.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:41:57.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000034",
            //            Debit = 45.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:53:50.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000035",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 09:56:07.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000036",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:19:06.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000037",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:20:17.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000038",
            //            Debit = 70.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:26:24.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000039",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:40:22.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000040",
            //            Debit = 875.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:40:22.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000040",
            //            Debit = 1760.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:46:00.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000041",
            //            Debit = 6000.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:49:56.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000042",
            //            Debit = 200.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:52:57.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000043",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:53:54.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000044",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:53:54.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000044",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 10:59:28.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000045",
            //            Debit = 50.30m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:02:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000046",
            //            Debit = 756.50m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:02:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000046",
            //            Debit = 969.99m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:06:12.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000047",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:15:22.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000048",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:21:33.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000049",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:23:16.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000050",
            //            Debit = 15000.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:28:23.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000051",
            //            Debit = 7.60m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:30:55.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000052",
            //            Debit = 33.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:36:40.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000053",
            //            Debit = 45.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:38:28.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000054",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:41:21.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000055",
            //            Debit = 225.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:42:46.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000056",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 11:59:07.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000057",
            //            Debit = 125.02m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:11:10.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000058",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:13:56.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000059",
            //            Debit = 70.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:19:31.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000060",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:26:21.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000061",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:26:36.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000062",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:28:50.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000063",
            //            Debit = 31.50m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:29:21.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11219").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000064",
            //            Debit = 1050.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 12:53:48.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000065",
            //            Debit = 950.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:12:11.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000066",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:14:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000067",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:15:07.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000068",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:33:09.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000069",
            //            Debit = 119.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:33:09.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000069",
            //            Debit = 233.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:33:09.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000069",
            //            Debit = 79.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:34:00.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000070",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 13:46:42.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000071",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 14:02:16.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000072",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 14:07:58.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000073",
            //            Debit = 75.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 14:31:41.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11220").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000074",
            //            Debit = 150.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 14:35:16.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000075",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:02:00.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000076",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:04:32.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000077",
            //            Debit = 47.78m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:06:19.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000078",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:08:26.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000079",
            //            Debit = 70.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:10:32.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000080",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:21:47.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000081",
            //            Debit = 1381.73m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:25:34.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000082",
            //            Debit = 32.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:25:34.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000082",
            //            Debit = 32.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:29:58.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000083",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:30:11.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000084",
            //            Debit = 80.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:33:08.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000085",
            //            Debit = 2000.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:36:41.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000086",
            //            Debit = 711.73m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:36:54.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000087",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 15:58:40.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000088",
            //            Debit = 4230.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 16:25:34.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000089",
            //            Debit = 20.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 16:31:51.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000090",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 16:40:44.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000091",
            //            Debit = 30.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 16:44:26.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000092",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 16:51:30.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000093",
            //            Debit = 15.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:04:20.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000094",
            //            Debit = 60.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:15:54.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000095",
            //            Debit = 118.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:20:38.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000096",
            //            Debit = 52.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:40:20.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000097",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:40:20.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000097",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:40:20.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000097",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:40:20.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000097",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 17:48:39.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000098",
            //            Debit = 118.38m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 18:00:01.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000099",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-01 18:00:27.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000100",
            //            Debit = 800.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 07:25:36.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000126",
            //            Debit = 133.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 07:47:39.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000127",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 07:48:28.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000128",
            //            Debit = 80.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 07:54:31.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000129",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:02:44.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000130",
            //            Debit = 2015.34m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:09:13.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000131",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:17:57.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000132",
            //            Debit = 1000.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:29:36.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000133",
            //            Debit = 50.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:46:46.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000134",
            //            Debit = 1500.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 08:49:57.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000135",
            //            Debit = 12.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 09:26:17.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000136",
            //            Debit = 7.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 09:46:43.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11203").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000137",
            //            Debit = 1300.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 10:31:04.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000138",
            //            Debit = 125.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 10:38:38.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000139",
            //            Debit = 110.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 11:25:13.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000141",
            //            Debit = 100.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 11:33:42.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000142",
            //            Debit = 2020.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 11:58:07.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000143",
            //            Debit = 70.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 12:04:22.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11206").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000144",
            //            Debit = 37.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 12:09:24.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11204").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000145",
            //            Debit = 10.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 12:18:02.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000146",
            //            Debit = 30.00m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 12:34:23.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000147",
            //            Debit = 94.74m,
            //            Kredit = 0
            //        },
            //        new AkAkaun
            //        {
            //            JKWId = kw.Id,
            //            AkCartaId1 = carta1.Id,
            //            Tarikh = DateTime.Parse("2021-01-02 13:01:19.000"),
            //            AkCartaId2 = context.AkCarta.Where(b => b.Kod == "H11301").FirstOrDefault().Id,
            //            NoRujukan = "RR/IB1002021000148",
            //            Debit = 30.00m,
            //            Kredit = 0
            //        }
            //    );
            //}
            //context.SaveChanges();
        }
    }
}