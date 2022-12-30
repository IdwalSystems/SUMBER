using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;
using SUMBER.Models.Helper;
using SUMBER.Models.Modules;

namespace SUMBER.Models.Sumber
{
    public class SuProfilGaji : AppLogHelper, ISoftDelete
    {
        //field
        public int Id { get; set; }
        [DisplayName("Anggota")]
        public int SuPekerjaId { get; set; }

        [DisplayName("Kod Gaji")]
        public int JSuKodGajiId { get; set; }

        [DisplayName("Amaun")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amaun { get; set; }

        //field end

        //relationship
        [DisplayName("Anggota")]
        public SuPekerja SuPekerja { get; set; }

        [DisplayName("Kod Gaji")]
        public JSuKodGaji JSuKodGaji { get; set; }
        //relationship end

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end
    }
}
