﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class JPenyemakRepository : IRepository<JPenyemak, int, string>
    {

        public readonly ApplicationDbContext context;

        public JPenyemakRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JPenyemak.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<JPenyemak>> GetAll()
        {
            return await context.JPenyemak.Include(b => b.SuPekerja).ToListAsync();
        }

        public async Task<IEnumerable<JPenyemak>> GetAllIncludeDeletedItems()
        {
            return await context.JPenyemak
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja).ToListAsync();
        }

        public async Task<JPenyemak> GetById(int id)
        {
            return await context.JPenyemak
                .Include(b => b.SuPekerja)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<JPenyemak> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JPenyemak
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JPenyemak> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JPenyemak> Insert(JPenyemak entity)
        {
            await context.JPenyemak.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JPenyemak entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
