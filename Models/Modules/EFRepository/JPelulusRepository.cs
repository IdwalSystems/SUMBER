﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class JPelulusRepository : IRepository<JPelulus, int, string>
    {
        public readonly ApplicationDbContext context;

        public JPelulusRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JPelulus.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<JPelulus>> GetAll()
        {
            return await context.JPelulus.Include(b => b.SuPekerja).ToListAsync();
        }

        public async Task<IEnumerable<JPelulus>> GetAllIncludeDeletedItems()
        {
            return await context.JPelulus
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja).ToListAsync();
        }

        public async Task<JPelulus> GetById(int id)
        {
            return await context.JPelulus
                .Include(b => b.SuPekerja)
                .FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<JPelulus> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JPelulus
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JPelulus> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JPelulus> Insert(JPelulus entity)
        {
            await context.JPelulus.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JPelulus entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
