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
    public class JKWRepository : IRepository<JKW, int, string>
    {
        private readonly ApplicationDbContext context;
        public JKWRepository(ApplicationDbContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var kw = await context.JKW.FirstOrDefaultAsync(b => b.Id == id);
            if (kw != null)
            {
                context.Remove(kw);
            }
        }

        public async Task<IEnumerable<JKW>> GetAll()
        {
            return await context.JKW.ToListAsync();
        }

        public async Task<IEnumerable<JKW>> GetAllIncludeDeletedItems()
        {
            return await context.JKW.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<JKW> GetById(int id)
        {
            return await context.JKW.FindAsync(id);
        }

        public async Task<JKW> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JKW.IgnoreQueryFilters().Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public Task<JKW> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JKW> Insert(JKW entity)
        {
            await context.JKW.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JKW entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
