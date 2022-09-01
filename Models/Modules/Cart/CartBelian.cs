﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.Cart
{
    public class CartBelian
    {
        //Belian 1

        private List<AkBelian1> collection1 = new List<AkBelian1>();

        public virtual void AddItem1(
            int akBelianId,
            decimal amaun,
            int akCartaId
            )
        {
            AkBelian1 line = collection1
            .Where(p => p.AkCartaId == akCartaId)
            .FirstOrDefault();

            if (line == null)
            {
                collection1.Add(new AkBelian1
                {
                    AkBelianId = akBelianId,
                    Amaun = amaun,
                    AkCartaId = akCartaId
                });
            }
        }

        public virtual void RemoveItem1(int id) =>
            collection1.RemoveAll(l => l.AkCartaId == id);


        public virtual void Clear1() => collection1.Clear();

        public virtual IEnumerable<AkBelian1> Lines1 => collection1;
        // Belian1 End

        //Belian 2
        private List<AkBelian2> collection2 = new List<AkBelian2>();

        public virtual void AddItem2(
            int akBelianId,
            int Indek,
            decimal Bil,
            string NoStok,
            string Perihal,
            decimal Kuantiti,
            string Unit,
            decimal Harga,
            decimal Amaun
            )
        {
            collection2.Add(new AkBelian2
            {
                AkBelianId = akBelianId,
                Indek = Indek,
                Bil = Bil,
                NoStok = NoStok,
                Perihal = Perihal,
                Kuantiti = Kuantiti,
                Unit = Unit,
                Harga = Harga,
                Amaun = Amaun
            });
        }

        public virtual void RemoveItem2(int id) =>
            collection2.RemoveAll(l => l.Indek == id);


        public virtual void Clear2() => collection2.Clear();

        public virtual IEnumerable<AkBelian2> Lines2 => collection2;
        // Belian 2 end
    }
}
