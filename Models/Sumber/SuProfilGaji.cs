using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;
using SUMBER.Models.Helper;
using SUMBER.Models.Modules;
using System.Collections.Generic;

namespace SUMBER.Models.Sumber
{
    public class SuProfilGaji
    {
        //field
        public int Id { get; set; }
        [DisplayName("Anggota")]
        public int SuPekerjaId { get; set; }
        [DisplayName("Kod Gaji")]
        public int JSuKodGajiId { get; set; }
        [DisplayName("Elaun RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Elaun { get; set; }
        [DisplayName("Potongan RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Potongan { get; set; }
        // note:
        // FlKWSP = 0 : Tidak
        // FlKWSP = 1 : Ya
        [DisplayName("KWSP")]
        public int FlKWSP { get; set; }

        //field end

        //relationship
        [DisplayName("Anggota")]
        public SuPekerja SuPekerja { get; set; }

        [DisplayName("Kod Gaji")]
        public JSuKodGaji JSuKodGaji { get; set; }
        //relationship end

    }
}
