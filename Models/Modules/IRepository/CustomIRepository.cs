﻿using SUMBER.Models.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.IRepository
{
    public interface CustomIRepository<T1, T2>
    {
        Task<decimal> GetBalanceFromAbBukuVot(T1 tahun, int? akCartaId, int jKW, int? jBahagian);
        Task<decimal> GetBalanceFromKaunterPanjar(T1 bakiAwal, T2 akTunaiRuncitId);
    }
}
