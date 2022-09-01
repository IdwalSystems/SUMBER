using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUMBER.Models.Modules
{
    public class JBank : AppLogHelper, ISoftDelete
    {
        //field
        public int Id { get; set; }
        [Required]
        [MaxLength(12)]
        public string Kod { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nama { get; set; }
        public string KodEFT { get; set; }
        public ICollection<AkBank> AkBank { get; set; }
        public ICollection<AkPembekal> AkPembekal { get; set; }
        public ICollection<AkCimbEFT1> AkCimbEFT1 { get; set; }
        public ICollection<AkPV> AkPV { get; set; }
        public ICollection<AkPVGanda> AkPVGanda { get; set; }
        //field end

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end
    }
}