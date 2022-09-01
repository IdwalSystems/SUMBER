﻿using System.Collections.Generic;

namespace SUMBER.Models.Modules
{
    public class AkPadananPenyata
    {
        // FlJenis
        // 1 - AkPV
        // 2 - AkTerima
        // 3 - AkJurnal
        public int Id { get; set; }
        public int AkBankReconPenyataBankId { get; set; }
        public ICollection<AkBankReconPenyataBank> AkBankRecon { get; set; }
        public int FlJenis { get; set; }
        public int? AkPVId { get; set; }
        public ICollection<AkPV> AkPV { get; set; }
        public int? AkTerima2Id { get; set; }
        public ICollection<AkTerima2> AkTerima2 { get; set; }
        

    }
}
