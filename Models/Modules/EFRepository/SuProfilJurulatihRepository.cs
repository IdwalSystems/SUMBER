﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{
    public class SuProfilJurulatihRepository : IRepository<SuProfil, int, string>
    {
        public readonly ApplicationDbContext context;

        public SuProfilJurulatihRepository(ApplicationDbContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var model = await context.SuProfil.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<SuProfil>> GetAll()
        {
            return await context.SuProfil.Where(x => x.FlKategori == 1).ToListAsync();
        }

        public async Task<IEnumerable<SuProfil>> GetAllIncludeDeletedItems()
        {
            return await context.SuProfil
                .Include(b => b.JKW)
                .Include(b => b.AkCarta)
                .Include(b => b.JBahagian)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuAtlet).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuJurulatih).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JSukan)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JCaraBayar)
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<SuProfil> GetById(int id)
        {
            return await context.SuProfil
                .Include(b => b.JKW)
                .Include(b => b.AkCarta)
                .Include(b => b.JBahagian)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuAtlet).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuJurulatih).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JSukan)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JCaraBayar)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SuProfil> GetByIdIncludeDeletedItems(int id)
        {
            return await context.SuProfil
                .Include(b => b.JKW)
                .Include(b => b.AkCarta)
                .Include(b => b.JBahagian)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuAtlet).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.SuJurulatih).ThenInclude(b => b.JBank)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JSukan)
                .Include(b => b.SuProfil1).ThenInclude(b => b.JCaraBayar)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SuProfil> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SuProfil> Insert(SuProfil entity)
        {
            await context.SuProfil.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(SuProfil entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
