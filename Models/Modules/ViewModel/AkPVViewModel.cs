﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.ViewModel
{
    public class AkPVViewModel : AkPV
    {
        public string KodPenerima { get; set; }
        public string Penerima { get; set; }
        public string CaraBayar { get; set; }
        public decimal JumlahInbois { get; set; }
    }
}
