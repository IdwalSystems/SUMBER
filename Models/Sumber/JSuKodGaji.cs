using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;

namespace SUMBER.Models.Sumber
{
    public class JSuKodGaji : AppLogHelper, ISoftDelete
    {
        //field
        public int ID { get; set; }
        [Required]
        [MaxLength(4)]

        public string Kod { get; set; }
        [Required]
        [MaxLength(100)]
        public string Perihal { get; set; }

        [DisplayName("Jenis")]
        [DataType(DataType.Text)]
        public int FlJenis { get; set; }

        //field end


        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
    }
}

