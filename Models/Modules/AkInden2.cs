﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUMBER.Models.Modules
{
    public class AkInden2
    {
        //field
        public int Id { get; set; }
        public int AkIndenId { get; set; }
        public int Indek { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bil { get; set; }
        [MaxLength(20)]
        public string NoStok { get; set; }
        [MaxLength(100)]
        public string Perihal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kuantiti { get; set; }
        [MaxLength(100)]
        public string Unit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Harga { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amaun { get; set; }
        //field end
    }
}
