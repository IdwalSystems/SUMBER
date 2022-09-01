﻿using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUMBER.Models.Modules
{
    public class JPTJ : ISoftDelete
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(3)]
        public string Kod { get; set; }
        [Required]
        [MaxLength(100)]
        public string Perihal { get; set; }
        public int? JKWId { get; set; }
        public JKW JKW { get; set; }
        public ICollection<JBahagian> JBahagian { get; set; }

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end
    }
}
