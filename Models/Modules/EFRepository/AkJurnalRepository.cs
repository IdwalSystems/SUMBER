﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class AkJurnalRepository : IRepository<AkJurnal, int, string>
    {
        public readonly ApplicationDbContext context;
        public AkJurnalRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.AkJurnal.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<AkJurnal>> GetAll()
        {
            return await context.AkJurnal
                .Include(b => b.JBahagian)
                .Include(b => b.AkTunaiRuncit)
                .Include(b => b.JKW)
                .Include(b => b.AkJurnal1)
                .ToListAsync();
        }

        public async Task<IEnumerable<AkJurnal>> GetAllIncludeDeletedItems()
        {
            return await context.AkJurnal
                .IgnoreQueryFilters()
                .Include(b => b.JBahagian)
                .Include(b => b.AkTunaiRuncit)
                .Include(b => b.JKW)
                .Include(b => b.AkJurnal1).ThenInclude(b => b.AkCarta)
                .ToListAsync();
        }

        public async Task<AkJurnal> GetById(int id)
        {
            return await context.AkJurnal
                .Include(b => b.JBahagian)
                .Include(b => b.AkTunaiRuncit)
                .Include(b => b.JKW)
                .Include(b => b.AkJurnal1).ThenInclude(b => b.AkCarta)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AkJurnal> GetByIdIncludeDeletedItems(int id)
        {
            return await context.AkJurnal
                .IgnoreQueryFilters()
                .Include(b => b.JBahagian)
                .Include(b => b.AkTunaiRuncit)
                .Include(b => b.JKW)
                .Include(b => b.AkJurnal1).ThenInclude(b=> b.AkCarta)
                .Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public Task<AkJurnal> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AkJurnal> Insert(AkJurnal entity)
        {
            await context.AkJurnal.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(AkJurnal entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
