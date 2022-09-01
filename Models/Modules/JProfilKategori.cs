﻿using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUMBER.Models.Modules
{
    public class JProfilKategori : AppLogHelper, ISoftDelete
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Perihal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Kadar Geran RM")]
        public decimal KadarGeran { get;set; }
        //public ICollection<SuJurulatih> SuJurulatih { get; set; }
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
    }
}
