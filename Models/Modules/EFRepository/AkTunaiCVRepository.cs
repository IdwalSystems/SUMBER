﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class AkTunaiCVRepository : IRepository<AkTunaiCV, int, string>
    {
        public readonly ApplicationDbContext context;

        public AkTunaiCVRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.AkTunaiCV.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<AkTunaiCV>> GetAll()
        {
            return await context.AkTunaiCV
                .Include(b => b.SuPekerja)
                .Include(b => b.AkPembekal)
                .Include(b=> b.AkTunaiRuncit).ThenInclude(b=> b.AkTunaiPemegang).ThenInclude(b=> b.SuPekerja)
                .Include(b=> b.AkTunaiRuncit).ThenInclude(b=> b.JKW)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JBahagian)
                .Include(b => b.AkTunaiCV1).ThenInclude(b=> b.AkCarta)
                .ToListAsync();
        }

        public async Task<IEnumerable<AkTunaiCV>> GetAllIncludeDeletedItems()
        {
            return await context.AkTunaiCV
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja)
                .Include(b => b.AkPembekal)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.AkTunaiPemegang).ThenInclude(b => b.SuPekerja)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JKW)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JBahagian)
                .Include(b => b.AkTunaiCV1).ThenInclude(b => b.AkCarta)
                .ToListAsync();
        }

        public async Task<AkTunaiCV> GetById(int id)
        {
            return await context.AkTunaiCV
                .Include(b => b.SuPekerja).ThenInclude(b=> b.JBank)
                .Include(b => b.AkPembekal).ThenInclude(b => b.JBank)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.AkTunaiPemegang).ThenInclude(b => b.SuPekerja)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JKW)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JBahagian)
                .Include(b => b.AkTunaiCV1).ThenInclude(b => b.AkCarta)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<AkTunaiCV> GetByIdIncludeDeletedItems(int id)
        {
            return await context.AkTunaiCV
                .IgnoreQueryFilters()
                .Include(b => b.SuPekerja).ThenInclude(b => b.JBank)
                .Include(b => b.AkPembekal).ThenInclude(b => b.JBank)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.AkTunaiPemegang).ThenInclude(b => b.SuPekerja)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JKW)
                .Include(b => b.AkTunaiRuncit).ThenInclude(b => b.JBahagian)
                .Include(b => b.AkTunaiCV1).ThenInclude(b => b.AkCarta)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<AkTunaiCV> GetByString(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AkTunaiCV> Insert(AkTunaiCV entity)
        {
            await context.AkTunaiCV.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(AkTunaiCV entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
