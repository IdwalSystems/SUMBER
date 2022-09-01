using SUMBER.Models.Administration;
using System.Collections.Generic;

namespace SUMBER.Models.Modules.PrintModel
{
    public class SuProfilAtletPrintModel
    {
        public string JumlahDalamPerkataan { get; set; }
        public string Username { get; set; }
        public SuProfil SuProfil { get; set; }
        public CompanyDetails CompanyDetail { get; set; }
    }
}
