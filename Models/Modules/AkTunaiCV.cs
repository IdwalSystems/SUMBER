﻿using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class AkTunaiCV : AppLogHelper, ISoftDelete
    {
        public int Id { get; set; }
        [DisplayName("Kod Kaunter Panjar")]
        public int AkTunaiRuncitId { get; set; }
        public AkTunaiRuncit AkTunaiRuncit { get; set; }
        public int KategoriPenerima { get; set; }
        public string Tahun { get; set; }
        [DisplayName("No CV")]
        public string NoCV { get; set; }
        public DateTime Tarikh { get; set; }
        [DisplayName("Kod Anggota")]
        public int? SuPekerjaId { get; set; }
        public SuPekerja SuPekerja { get; set; }
        [DisplayName("Kod Pembekal")]
        public int? AkPembekalId { get; set; }
        public AkPembekal AkPembekal { get; set; }
        [DisplayName("No KP")]
        public string NoKP { get; set; }
        public string Penerima { get; set; }
        [DisplayName("Alamat")]
        public string Alamat1 { get; set; }
        public string Alamat2 { get; set; }
        public string Alamat3 { get; set; }
        public string Catatan { get; set; }
        [DisplayName("Jumlah RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jumlah { get; set; }
        public ICollection<AkTunaiCV1> AkTunaiCV1 { get; set; }
        public int FlPosting { get; set; }
        public DateTime? TarikhPosting { get; set; }
        public int FlCetak { get; set; }
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }

    }
}
