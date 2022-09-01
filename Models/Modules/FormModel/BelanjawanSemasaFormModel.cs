﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SUMBER.Models.Modules.FormModel
{
    public class BelanjawanSemasaFormModel
    {
        [Display(Name = "Kumpulan Wang")]
        public int JKWId { get; set; }
        [Display(Name = "Bahagian")]
        public int JBahagianId { get; set; }
        [Required(ErrorMessage = "Tahun Diperlukan")]
        public string Tahun { get; set; }
        [Required(ErrorMessage = "Tarikh Diperlukan")]
        [Display(Name = "Tarikh Hingga")]
        public DateTime TarHingga { get; set; }
    }
}
