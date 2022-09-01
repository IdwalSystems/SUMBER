﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    [Authorize]
    public class JAgamaRepository : IRepository<JAgama, int, string>
    {
        private readonly ApplicationDbContext context;
        public JAgamaRepository(ApplicationDbContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var agama = await context.JAgama.FirstOrDefaultAsync(b => b.Id == id);
            if (agama != null)
            {
                context.Remove(agama);
            }
        }

        public async Task<IEnumerable<JAgama>> GetAll()
        {
            return await context.JAgama.ToListAsync();
        }

        public async Task<IEnumerable<JAgama>> GetAllIncludeDeletedItems()
        {
            return await context.JAgama.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<JAgama> GetById(int id)
        {
            return await context.JAgama.FindAsync(id);
        }

        public async Task<JAgama> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JAgama.IgnoreQueryFilters().FirstOrDefaultAsync(x=> x.Id == id);
        }

        public Task<JAgama> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JAgama> Insert(JAgama entity)
        {
            await context.JAgama.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JAgama entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
