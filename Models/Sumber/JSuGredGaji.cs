using System;
using System.ComponentModel.DataAnnotations;
using SUMBER.Models.Helper;

namespace SUMBER.Models.Sumber
{
    public class JSuGredGaji : AppLogHelper, ISoftDelete
    {

        //field
        public int Id { get; set; }
        [Required]
        [MaxLength(2)]
        public string Kod { get; set; }
        [Required]
        [MaxLength(100)]
        public string Perihal { get; set; }
        //field end

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end

    }
}
