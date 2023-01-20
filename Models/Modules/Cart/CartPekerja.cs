using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUMBER.Models.Sumber;

namespace SUMBER.Models.Modules.Cart
{
    public class CartPekerja
    {
        private List<SuTanggunganPekerja> collection1 = new List<SuTanggunganPekerja>();
        public virtual void AddItem1(
            int SuPekerjaId,
            string Nama,
            string Hubungan,
            string NoKP)
        {
            SuTanggunganPekerja line = collection1
            .Where(p => p.NoKP == NoKP)
            .FirstOrDefault();

            if (line == null)
            {
                collection1.Add(new SuTanggunganPekerja
                {
                    SuPekerjaId = SuPekerjaId,
                    Nama = Nama,
                    Hubungan = Hubungan,
                    NoKP = NoKP
                });
            }

        }
        public virtual void RemoveItem1(string nokp) =>
            collection1.RemoveAll(l => l.NoKP == nokp);

        public virtual void Clear1() => collection1.Clear();

        public virtual IEnumerable<SuTanggunganPekerja> Lines1 => collection1;

        // Profil Gaji

        private List<SuProfilGaji> collection2 = new List<SuProfilGaji>();

        public virtual void AddItem2(
            int suPekerjaId,
            int jSuKodGajiId,
            decimal elaun,
            decimal potongan,
            int flKWSP
            )
        {
            SuProfilGaji line = collection2
            .Where(p => p.JSuKodGajiId == jSuKodGajiId)
            .FirstOrDefault();

            if (line == null)
            {
                collection2.Add(new SuProfilGaji
                {
                    SuPekerjaId = suPekerjaId,
                    JSuKodGajiId = jSuKodGajiId,
                    Elaun = elaun,
                    Potongan = potongan,
                    FlKWSP = flKWSP
                });
            }
        }

        public virtual void RemoveItem2(int id) =>
            collection2.RemoveAll(l => l.JSuKodGajiId == id);


        public virtual void Clear2() => collection2.Clear();

        public virtual IEnumerable<SuProfilGaji> Lines2 => collection2;
        // ProfilGaji 1 End

    }
}
