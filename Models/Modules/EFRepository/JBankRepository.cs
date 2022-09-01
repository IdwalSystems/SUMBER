﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class JBankRepository : IRepository<JBank, int, string>
    {
        public readonly ApplicationDbContext context;

        public JBankRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var bank = await context.JBank.FirstOrDefaultAsync(b => b.Id == id);
            if (bank != null)
            {
                context.Remove(bank);
            }
        }

        public async Task<IEnumerable<JBank>> GetAll()
        {
            return await context.JBank.ToListAsync();
        }

        public async Task<IEnumerable<JBank>> GetAllIncludeDeletedItems()
        {
            return await context.JBank.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<JBank> GetById(int id)
        {
            return await context.JBank.FindAsync(id);
        }

        public async Task<JBank> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JBank
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JBank> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JBank> Insert(JBank entity)
        {
            await context.JBank.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JBank entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
