﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SUMBER.Models.Modules.AppLog;

namespace SUMBER.Models.Modules
{
    public class AppLog
    {
        public int Id { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Tarikh")]
        public DateTime LgDate { get; set; }
        [DisplayName("Modul")]
        public string LgModule { get; set; }
        [DisplayName("Operasi")]
        public string LgOperation { get; set; }
        [DisplayName("Nota")]
        public string LgNote { get; set; }
        [DisplayName("No Rujukan")]
        public string NoRujukan { get; set; }
        [DisplayName("Id Rujukan")]
        public int IdRujukan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jumlah { get; set; }
        public string SysCode { get; set; }
        public int? SuPekerjaId { get; set; }
        
    }
}
