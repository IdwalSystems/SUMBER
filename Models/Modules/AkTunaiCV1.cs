﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class AkTunaiCV1
    {
        //field
        public int Id { get; set; }
        public int AkTunaiCVId { get; set; }
        [DisplayName("Kod Objek")]
        public int AkCartaId { get; set; }
        [DisplayName("Amaun RM")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amaun { get; set; }
        //field end

        //Relationship
        public AkCarta AkCarta { get; set; }
        //relationship end
    }
}
