﻿using SUMBER.Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.PrintModel
{
    public class NotaMintaPrintModel 
    {
        public string JumlahDalamPerkataan { get; set; }
        public decimal JumlahPerihal { get; set; }
        public string TarikhKewangan { get; set; }
        public string username { get; set; }
        public AkNotaMinta AkNotaMinta { get; set; }
        public CompanyDetails CompanyDetail { get; set; }
    }
}
