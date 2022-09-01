﻿using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUMBER.Models.Modules.EFRepository
{

    public class AkPO1Repository : ListViewIRepository<AkPO1, int>
    {
        public readonly ApplicationDbContext context;

        public AkPO1Repository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.AkPO1.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<AkPO1>> GetAll(int akPOId)
        {
            return await context.AkPO1
                .Include(b => b.AkCarta)
                .Where(x => x.AkPOId == akPOId)
                .ToListAsync();
        }

        public async Task<AkPO1> GetBy2Id(int akPOId, int akCartaId)
        {
            return await context.AkPO1.FirstOrDefaultAsync(x => x.AkPOId == akPOId && x.AkCartaId == akCartaId);
        }

        public async Task<AkPO1> GetById(int id)
        {
            return await context.AkPO1.Include(x=> x.AkCarta)
                .Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AkPO1> Insert(AkPO1 entity)
        {
            await context.AkPO1.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(AkPO1 entity)
        {

            AkPO1 data = context.AkPO1.FirstOrDefault(x => x.Id == entity.Id);
            data.Amaun = entity.Amaun;
            //Tambah kalau ada data dalam field lagi
            await context.SaveChangesAsync();
        }
    }
}
