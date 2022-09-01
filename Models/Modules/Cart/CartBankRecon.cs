﻿using System;
using System.Collections.Generic;

namespace SUMBER.Models.Modules.Cart
{
    public class CartBankRecon
    {
        // BankRecon 1
        private List<AkBankReconPenyataBank> collection1 = new List<AkBankReconPenyataBank>();

        public virtual void AddItem1(
            int indek,
            int akBankReconId,
            string noAkaunBank,
            DateTime tarikh,
            string kodTransaksi,
            string perihalTransaksi,
            string noDokumen,
            decimal debit,
            decimal kredit,
            decimal baki,
            int akPadananPenyataId)
        {
            collection1.Add(new AkBankReconPenyataBank
            {
                Indek = indek,
                AkBankReconId = akBankReconId,
                NoAkaunBank = noAkaunBank,
                Tarikh = tarikh,
                KodTransaksi = kodTransaksi,
                PerihalTransaksi = perihalTransaksi,
                NoDokumen = noDokumen,
                Debit = debit,
                Kredit = kredit,
                Baki = baki,
                AkPadananPenyataId = akPadananPenyataId
            });
        }

        public virtual void RemoveItem1(int id) =>
            collection1.RemoveAll(l => l.Indek == id);

        public virtual void Clear1() => collection1.Clear();

        public virtual IEnumerable<AkBankReconPenyataBank> Lines1 => collection1;
        // BankRecon 1 end
    }
}
