using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SUMBER.Models.Login.ViewModel
{
    public class UploadImageViewModel
    {
        [Display(Name = "Logo")]
        public IFormFile Gambar { get; set; }
    }
}
