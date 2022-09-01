﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUMBER.Models.Modules
{
    public class AkInden1
    {
        //field
        public int Id { get; set; }
        public int AkIndenId { get; set; }
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
