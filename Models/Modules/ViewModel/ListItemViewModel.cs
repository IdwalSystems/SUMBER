﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.ViewModel
{
    public class ListItemViewModel
    {
        [NotMapped]
        public int id { get; set; }
        [NotMapped]
        public string perihal { get; set; }
    }
}
