﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SUMBER.Models.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Administration
{
    public class ApplicationUser : IdentityUser
    {

        public string Nama { get; set; }
        [DisplayName("Tandatangan")]
        public string Tandatangan { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public List<string> UserRoles { get; set; }
        [NotMapped]
        public List<IdentityUserRole<string>> SelectedRoleList { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> RoleList { get; set; }

        //relationship
        public int? SuPekerjaId { get; set; }
        public SuPekerja SuPekerja { get; set; }
        [DisplayName("Bahagian")]
        public string JBahagianList { get; set; }
        [NotMapped]
        public List<int> SelectedJBahagianList { get; set; }

    }
}
