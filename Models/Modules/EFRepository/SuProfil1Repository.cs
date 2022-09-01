﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class SuProfil1Repository : IRepository<SuProfil1, int, string>
    {
        public readonly ApplicationDbContext context;

        public SuProfil1Repository(ApplicationDbContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var model = await context.SuProfil1.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<SuProfil1>> GetAll()
        {
            return await context.SuProfil1.ToListAsync();
        }

        public async Task<IEnumerable<SuProfil1>> GetAllIncludeDeletedItems()
        {
            return await context.SuProfil1
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<SuProfil1> GetById(int id)
        {
            return await context.SuProfil1.FindAsync(id);

        }

        public async Task<SuProfil1> GetByIdIncludeDeletedItems(int id)
        {
            return await context.SuProfil1
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SuProfil1> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<SuProfil1> Insert(SuProfil1 entity)
        {
            await context.SuProfil1.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(SuProfil1 entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
