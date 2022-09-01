﻿using Microsoft.AspNetCore.Mvc;
using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class AkBelian : AppLogHelper, ISoftDelete
    {
        //field
        public int Id { get; set; }
        [Required(ErrorMessage = "Tahun Diperlukan.")]
        [MaxLength(4)]
        public string Tahun { get; set; }
        [Required(ErrorMessage = "Tarikh Diperlukan")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarikh { get; set; }
        // Tarikh Terima Bahagian
        [DisplayName("Tarikh Terima")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TarikhTerima { get; set; }
        [DisplayName("Tarikh Kewangan Terima")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TarikhKewanganTerima { get; set; }
        public DateTime? TarikhPosting { get; set; }
        [DisplayName("No Rujukan")]
        [Required(ErrorMessage = "No Rujukan Diperlukan")]
        public string NoInbois { get; set; }
        [BindProperty]
        [DisplayName("Jumlah RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jumlah { get; set; }
        //field end

        //flag
        [DisplayName("Posting")]
        [DefaultValue("0")]
        public int FlPosting { get; set; }
        [DisplayName("Batal")]
        [DefaultValue("0")]
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        // if have akPOId or akIndenId, Dengan Tanggungan
        // else Tanpa Tanggungan
        [DisplayName("Dengan Tanggungan/Tanpa Tanggungan")]
        [DefaultValue("1")]
        public string FlTanggungan { get; set; }
        // note :
        // FlJenisTanggungan = 0 : Tanpa Tanggungan
        // FlJenisTanggungan = 1 : PO
        // FlJenisTanggungan = 2 : Inden Kerja
        [DisplayName("Inden Kerja / Pesanan Tempatan")]
        public int FlJenisTanggungan { get; set; }
        //flag end

        //Relationship
        [Required(ErrorMessage = "Jenis Kumpulan Wang Diperlukan.")]
        [DisplayName("Kumpulan Wang")]
        public int JKWId { get; set; }
        [DisplayName("Bahagian")]
        public int? JBahagianId { get; set; }
        public JBahagian JBahagian { get; set; }
        [DisplayName("No Pesanan Tempatan")]
        public int? AkPOId { get; set; }
        [DisplayName("No Inden")]
        public int? AkIndenId { get; set; }
        [Required(ErrorMessage = "Kod Pemiutang Diperlukan.")]
        [DisplayName("Kod Pemiutang")]
        public int KodObjekAPId { get; set; }
        [Required(ErrorMessage = "Kod Pembekal Diperlukan.")]
        [DisplayName("Kod Pembekal")]
        public int AkPembekalId { get; set; }
        public JKW JKW { get; set; }
        public AkPO AkPO { get; set; }
        public AkInden AkInden { get; set; }
        public AkCarta KodObjekAP { get; set; }
        public AkPembekal AkPembekal { get; set; }
        public ICollection<AkBelian1> AkBelian1 { get; set; }
        public ICollection<AkBelian2> AkBelian2 { get; set; }
        public ICollection<AkPV2> AkPV2 { get; set; }
        
        //relationship end
    }
}
