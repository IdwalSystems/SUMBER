using SUMBER.Models.Helper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SUMBER.Models.Sumber
{
    public class JSuPCB : AppLogHelper, ISoftDelete
    {
        public int Id { get; set; }

        [DisplayName("Gaji Awal")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GajiAwal { get; set; }

        [DisplayName("Gaji Akhir")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GajiAkhir { get; set; }
        // note:
        // FlKategori = 0 : Bujang
        // FlKategori = 1 : Kahwin & Jika isteri/suami tidak bekerja
        // FlKategori = 2 : Kahwin & Jika isteri/suami bekerja
        [DisplayName("Kategori")]
        [DataType(DataType.Text)]
        public int FlKategori { get; set; }

        [DisplayName("Potongan Bujang")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Bujang { get; set; }

        [DisplayName("Potongan Kahwin")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Kahwin { get; set; }


        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }

    }
}