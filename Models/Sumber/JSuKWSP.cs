using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using SUMBER.Models.Helper;

namespace SUMBER.Models.Sumber
{
    public class JSuKWSP : AppLogHelper, ISoftDelete
    {
        //field
        public int Id { get; set; }

        [DisplayName("Gaji Awal")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GajiAwal { get; set; }

        [DisplayName("Gaji Akhir")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GajiAkhir { get; set; }

        [DisplayName("Potong Pekerja")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PotongPekerja { get; set; }

        [DisplayName("Potong Majikan")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PotongMajikan { get; set; }

        //field end

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end

    }
}
