﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class SpPendahuluanPelbagai2
    {
        public int Id { get; set; }
        public int SpPendahuluanPelbagaiId { get; set; }
        public int Indek { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Baris { get; set; }
        public string Perihal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kadar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bil { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bulan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jumlah { get; set; }

    }
}
