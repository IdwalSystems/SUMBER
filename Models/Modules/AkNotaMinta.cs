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
    public class AkNotaMinta : AppLogHelper, ISoftDelete
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tahun Diperlukan.")]
        [MaxLength(4)]
        public string Tahun { get; set; }
        [Required(ErrorMessage = "Tarikh Diperlukan")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarikh { get; set; }
        public string NoRujukan { get; set; }
        public string Tajuk { get; set; }
        [DisplayName("Jumlah RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jumlah { get; set; }

        //flag
        //note :
        // FlJenis = 0 -> Pesanan Tempatan
        // FlJenis = 1 -> Inden Kerja
        [DisplayName("Inden Kerja / Pesanan Tempatan")]
        public int FlJenis { get; set; }
        [DisplayName("Posting")]
        [DefaultValue("0")]
        public int FlPosting { get; set; }
        public DateTime? TarikhPosting { get; set; }
        [DisplayName("Batal")]
        [DefaultValue("0")]
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        [DisplayName("Cetak")]
        [DefaultValue("0")]
        public int FlCetak { get; set; }
        //flag end

        // untuk kewangan
        [DisplayName("No Siri")]
        public string NoSiri{ get; set; }
        [DisplayName("No CAS")] // no PO
        public string NoCAS { get; set; }
        [DisplayName("Tarikh Seksyen Kewangan")]
        public DateTime? TarikhSeksyenKewangan { get; set; }
        // untuk kewangan end

        //untuk kelulusan
        [DisplayName("Penyemak")]
        public int? JPenyemakId { get; set; }
        public JPenyemak JPenyemak { get; set; }
        [DisplayName("Status Semak")]
        public int FlStatusSemak { get; set; }
        public DateTime? TarSemak { get; set; }
        [DisplayName("Pelulus")]
        public int? JPelulusId { get; set; }
        public JPelulus JPelulus { get; set; }
        [DisplayName("Status Lulus")]
        public int FlStatusLulus { get; set; }
        public DateTime? TarLulus { get; set; }
        //untuk kelulusan end 

        [DisplayName("Kumpulan Wang")]
        public int JKWId { get; set; }
        public JKW JKW { get; set; }

        [DisplayName("Bahagian")]
        public int? JBahagianId { get; set; }
        public JBahagian JBahagian { get; set; }

        [DisplayName("Kod Pembekal")]
        public int AkPembekalId { get; set; }
        public AkPembekal AkPembekal { get; set; }
        public ICollection<AkNotaMinta1> AkNotaMinta1 { get; set; }
        public ICollection<AkNotaMinta2> AkNotaMinta2 { get; set; }
        public ICollection<AkPO> AkPO { get; set; }
        public ICollection<AkInden> AkInden { get; set; }

    }
}
