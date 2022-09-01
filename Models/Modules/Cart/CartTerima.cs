﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SUMBER.Models.Modules.Cart
{
    public class CartTerima
    {
        //Terima1
        
        private List<AkTerima1> collection1 = new List<AkTerima1>();

        public virtual void AddItem1(
            int akTerimaId,
            decimal amaun,
            int akCartaId
            )
        {
            AkTerima1 line = collection1
            .Where(p => p.AkCartaId == akCartaId)
            .FirstOrDefault();

            if (line == null)
            {
                collection1.Add(new AkTerima1
                {
                    AkTerimaId = akTerimaId,
                    Amaun = amaun,
                    AkCartaId = akCartaId

                });
            }
        }

        public virtual void RemoveItem1(int id) =>
            collection1.RemoveAll(l => l.AkCartaId == id);


        public virtual void Clear1() => collection1.Clear();

        public virtual IEnumerable<AkTerima1> Lines1 => collection1;
        // Terima1 End

        //Terima2
        private List<AkTerima2> collection2 = new List<AkTerima2>();

        public virtual void AddItem2(
            int akTerimaId,
            int jCaraBayarId,
            decimal amaun, string noCek,
            int jenisCek, string kodBankCek,
            string tempatCek, string noSlip,
            DateTime? tarSlip,
            int? AkPenyataPemungutId
            )
        {
            AkTerima2 line = collection2
            .Where(p => p.JCaraBayarId == jCaraBayarId)
            .FirstOrDefault();

            if (line == null)
            {
                collection2.Add(new AkTerima2
                {
                    AkTerimaId = akTerimaId,
                    JCaraBayarId = jCaraBayarId,
                    Amaun = amaun,
                    NoCek = noCek,
                    JenisCek = jenisCek,
                    KodBankCek = kodBankCek,
                    TempatCek = tempatCek,
                    NoSlip = noSlip,
                    TarSlip = tarSlip,
                    AkPenyataPemungutId = AkPenyataPemungutId
                });
            }
        }

        public virtual void RemoveItem2(int id) =>
            collection2.RemoveAll(l => l.JCaraBayarId == id);


        public virtual void Clear2() => collection2.Clear();

        public virtual IEnumerable<AkTerima2> Lines2 => collection2;

        //Terima3

        private List<AkTerima3> collection3 = new List<AkTerima3>();

        public virtual void AddItem3(
            int akTerimaId,
            int? akInvoisId,
            decimal amaun
            )
        {
            AkTerima3 line = collection3
            .Where(p => p.AkInvoisId == akInvoisId)
            .FirstOrDefault();

            if (line == null)
            {
                collection3.Add(new AkTerima3
                {
                    AkTerimaId = akTerimaId,
                    AkInvoisId = akInvoisId,
                    Amaun = amaun

                });
            }
        }

        public virtual void RemoveItem3(int id) =>
            collection3.RemoveAll(l => l.AkInvoisId == id);


        public virtual void Clear3() => collection3.Clear();

        public virtual IEnumerable<AkTerima3> Lines3 => collection3;
        // Terima3 End

    }
}
