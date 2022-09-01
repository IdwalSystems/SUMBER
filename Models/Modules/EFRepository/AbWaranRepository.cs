﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class AbWaranRepository : IRepository<AbWaran, int, string>
    {
        public readonly ApplicationDbContext context;

        public AbWaranRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.AbWaran.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<AbWaran>> GetAll()
        {
            return await context.AbWaran
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AbWaran1).ThenInclude(b => b.AkCarta)
                .ToListAsync();
        }

        public async Task<IEnumerable<AbWaran>> GetAllIncludeDeletedItems()
        {
            return await context.AbWaran
                .IgnoreQueryFilters()
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AbWaran1).ThenInclude(b => b.AkCarta)
                .ToListAsync();
        }

        public async Task<AbWaran> GetById(int id)
        {
            return await context.AbWaran
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AbWaran1).ThenInclude(b => b.AkCarta)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<AbWaran> GetByIdIncludeDeletedItems(int id)
        {
            return await context.AbWaran
                .IgnoreQueryFilters()
                .Include(b => b.JKW)
                .Include(b => b.JBahagian)
                .Include(b => b.AbWaran1).ThenInclude(b => b.AkCarta)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<AbWaran> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AbWaran> Insert(AbWaran entity)
        {
            await context.AbWaran.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(AbWaran entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
