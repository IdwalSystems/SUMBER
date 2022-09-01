﻿using SUMBER.Models.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.IRepository
{
    public interface BelanjawanSemasaIRepository<T1, T2>
    {
        Task<List<AbWaran>> GetAbWaranBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        List<AbBelanjawanSemasaViewModel> RunWaranObjekOperation(
            int JenisWaran,
            string TK,
            decimal Amaun,
            string KodCarta,
            string Perihal,
            string Paras);

        Task<List<AkPO>> GetAkPOBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        Task<List<AkPOLaras>> GetAkPOLarasBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        Task<List<AkInden>> GetAkIndenBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        Task<List<AkTunaiCV>> GetAkTunaiCVBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        List<AbBelanjawanSemasaViewModel> RunPOPOLarasIndenCVObjekOperation(
            decimal Amaun,
            string KodCarta,
            string Perihal,
            string Paras);

        Task<List<AkPV>> GetAkPVBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        List<AbBelanjawanSemasaViewModel> RunBaucerObjekOperation(
           bool Tanggungan,
           decimal Amaun,
           string KodCarta,
           string Perihal,
           string Paras);

        Task<List<AkTerima>> GetAkTerimaBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        List<AbBelanjawanSemasaViewModel> RunResitObjekOperation(
           decimal Amaun,
           string KodCarta,
           string Perihal,
           string Paras);

        Task<List<AkJurnal>> GetAkJurnalBasedOnYear(T1 tahun, T2 jKWId, int jBahagianId, DateTime tarHingga);

        List<AbBelanjawanSemasaViewModel> RunJurnalObjekOperation(
           decimal Debit,
           decimal Kredit,
           string KodCarta,
           string Perihal,
           string Paras);
    }
}
