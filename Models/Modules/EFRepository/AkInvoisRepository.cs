﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class AkInvoisRepository : IRepository<AkInvois, int, string>
    {
        public readonly ApplicationDbContext context;

        public AkInvoisRepository(ApplicationDbContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var model = await context.AkInvois.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<AkInvois>> GetAll()
        {
            return await context.AkInvois
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AkPenghutang).ThenInclude(b=> b.JNegeri)
                .Include(b => b.KodObjekAP)
                .Include(b => b.AkInvois1).ThenInclude(b => b.AkCarta)
                .Include(b => b.AkInvois2)
                .ToListAsync();
        }

        public async Task<IEnumerable<AkInvois>> GetAllIncludeDeletedItems()
        {
            return await context.AkInvois
                .IgnoreQueryFilters()
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AkPenghutang).ThenInclude(b => b.JNegeri)
                .Include(b => b.KodObjekAP)
                .Include(b => b.AkInvois1).ThenInclude(b => b.AkCarta)
                .Include(b => b.AkInvois2)
                .ToListAsync();
        }

        public async Task<AkInvois> GetById(int id)
        {
            return await context.AkInvois.Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AkPenghutang).ThenInclude(b => b.JNegeri)
                .Include(b => b.KodObjekAP)
                .Include(b => b.AkInvois1).ThenInclude(b => b.AkCarta)
                .Include(b => b.AkInvois2)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<AkInvois> GetByIdIncludeDeletedItems(int id)
        {
            return await context.AkInvois.Include(b => b.JKW)
                .IgnoreQueryFilters()
                .Include(b => b.JBahagian)
                .Include(b => b.AkPenghutang).ThenInclude(b => b.JNegeri)
                .Include(b => b.KodObjekAP)
                .Include(b => b.AkInvois1).ThenInclude(b => b.AkCarta)
                .Include(b => b.AkInvois2)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<AkInvois> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AkInvois> Insert(AkInvois entity)
        {
            await context.AkInvois.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(AkInvois entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
