﻿using SUMBER.Models.Helper;
using SUMBER.Models.Sumber;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules
{
    public class SuPekerja : AppLogHelper, ISoftDelete

    {
        public int Id { get; set; }
        [DisplayName("No Gaji")]
        public string NoGaji { get; set; }
        [Required(ErrorMessage = "No Kad Pengenalan Diperlukan")]
        [DisplayName("No KP")]
        public string NoKp { get; set; }
        public string Nama { get; set; }
        [DisplayName("Alamat")]
        public string Alamat1 { get; set; }
        public string Alamat2 { get; set; }
        public string Alamat3 { get; set; }
        public string Poskod { get; set; }
        public string Bandar { get; set; }
        [DisplayName("Negeri")]
        [Required(ErrorMessage = "Negeri Diperlukan")]
        public int JNegeriId { get; set; }
        [DisplayName("No Telefon Rumah")]
        public string TelefonRumah { get; set; }
        [DisplayName("No Telefon Bimbit")]
        public string TelefonBimbit { get; set; }
        [Required(ErrorMessage = "Emel Diperlukan")]
        [EmailAddress(ErrorMessage = "Emel Tidak Sah."), MaxLength(100)]
        public string Emel { get; set; }
        [DefaultValue("0")]
        [DisplayName("Status Perkahwinan")]
        public int StatusKahwin { get; set; }
        [DefaultValue("0")]
        [DisplayName("Bilangan Anak")]
        public int BilAnak { get; set; }
        [DisplayName("Gaji Pokok")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GajiPokok { get; set; }
        [DisplayName("Tarikh Masuk Kerja")]
        public DateTime TarikhMasukKerja { get; set; }
        [DisplayName("Tarikh Berhenti Kerja")]
        public DateTime? TarikhBerhentiKerja { get; set; }
        [DisplayName("Tarikh Pencen")]
        public DateTime? TarikhPencen { get; set; }
        [DisplayName("Nama Bank")]
        public int JBankId { get; set; }
        [DisplayName("Agama")]
        public int? JAgamaId { get; set; }
        [DisplayName("Bangsa")]
        public int? JBangsaId { get; set; }
        [DisplayName("Jawatan")]
        public string Jawatan { get; set; }
        [DisplayName("Cara Bayar")]
        public int? JCaraBayarId { get; set; }
        [DisplayName("No Akaun Bank")]
        public string NoAkaunBank { get; set; }
        public bool IsAdmin { get; set; }
        [DisplayName("Taraf Jawatan")]
        public int JSuTarafJawatanId { get; set; }

        //relationship
        [DisplayName("Negeri")]
        public JNegeri JNegeri { get; set; }
        [DisplayName("Agama")]
        public JAgama JAgama { get; set; }
        [DisplayName("Nama Bank")]
        public JBank JBank { get; set; }
        [DisplayName("Bangsa")]
        public JBangsa JBangsa { get; set; }
       
        public ICollection<SuTanggunganPekerja> SuTanggungan { get; set; }
        [DisplayName("Cara Bayar")]
        public JCaraBayar JCaraBayar { get; set; }
        public ICollection<AkPV> AkPV { get; set; }
        public ICollection<AkTunaiCV> AkTunaiCV { get; set; }
        public ICollection<SpPendahuluanPelbagai> SpPendahuluanPelbagai { get; set; }
        public ICollection<JPelulus> JPelulus { get; set; }
        public ICollection<JPenyemak> JPenyemak { get; set; }
        public ICollection<AkCimbEFT> AkCimbEFT { get; set; }
        public ICollection<AkCimbEFT1> AkCimbEFT1 { get; set; }
        public ICollection<AkPenyataPemungut> AkPenyataPemungut { get; set; }
        public ICollection<AkPVGanda> AkPVGanda { get; set; }
        public ICollection<JSuTarafJawatan> JSuTarafJawatan { get; set; }
        //relationship end

        //soft delete
        public int FlHapus { get; set; }
        public DateTime? TarHapus { get; set; }
        //soft delete end
    }
}
