﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.ViewModel
{
    public class AkNotaMintaViewModel : AkNotaMinta
    {
        public string NamaSykt { get; set; }
        public string Alamat1 { get; set; }
        public decimal JumlahPerihal { get; set; }
    }
}
