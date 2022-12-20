using SUMBER.Models.Helper;
using SUMBER.Models.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUMBER.Models.Sumber
{
    public class JSuTarafJawatan : AppLogHelper, ISoftDelete
    {
        //field
        public int ID { get; set; }
        [Required]
        [MaxLength(2)]

        public string Kod { get; set; }
        [Required]
        [MaxLength(100)]
        public string Perihal { get; set; }
        //field end

        public ICollection<SuPekerja> SuPekerja { get; set; }

        public int FlHapus { get; set; }
        public DateTime? TarHapus { get ; set; }
    }
}
