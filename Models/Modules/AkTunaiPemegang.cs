﻿using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class AkTunaiPemegang : AppLogHelper, ISoftDelete
    {
        public int Id { get; set; }
        [DisplayName("Kod Kaunter Panjar")]
        public int AkTunaiRuncitId { get; set; }
        [DisplayName("Kod Anggota")]
        public int SuPekerjaId { get; set; }
        public SuPekerja SuPekerja { get; set; }

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end
    }
}
