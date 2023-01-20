using SUMBER.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using SUMBER.Models.Modules.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SUMBER.Models.Modules;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class SuProfilGajiRepository : ListViewIRepository<SuProfilGaji, int>
    {

        public readonly ApplicationDbContext context;

        public SuProfilGajiRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.SuProfilGaji.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<SuProfilGaji>> GetAll(int suPekerjaId)
        {
            //throw new System.NotImplementedException();
            return await context.SuProfilGaji.Include(b => b.JSuKodGaji)
                .Where(x => x.SuPekerjaId == suPekerjaId)
                .ToArrayAsync();
        }

        //public async Task<IEnumerable<SuProfilGaji>> GetAllIncludeDeletedItems()
        //{
        //    return await context.SuProfilGaji
        //        .IgnoreQueryFilters()
        //        .ToListAsync();
        //}

        public async Task<SuProfilGaji> GetBy2Id(int suPekerjaId, int jSuKodGajiId)
        {
            return await context.SuProfilGaji
                .FirstOrDefaultAsync(x => x.SuPekerjaId == suPekerjaId && x.JSuKodGajiId == jSuKodGajiId);
        }

        public async Task<SuProfilGaji> GetById(int id)
        {
            //return await context.SuProfilGaji.FindAsync(id);
            return await context.SuProfilGaji
                .Include(d => d.JSuKodGaji)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        //public async Task<SuProfilGaji> GetByIdIncludeDeletedItems(int id)
        //{
        //    return await context.SuProfilGaji
        //       .IgnoreQueryFilters()
        //       .FirstOrDefaultAsync(x => x.Id == id);
        //}

        //public Task<SuProfilGaji> GetByString(string id)
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task<SuProfilGaji> Insert(SuProfilGaji entity)
        {
            await context.SuProfilGaji.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(SuProfilGaji entity)
        {
            //context.Update(entity);
            SuProfilGaji data = context.SuProfilGaji.FirstOrDefault(x => x.Id == entity.Id);
            data.FlKWSP = entity.FlKWSP;
            
            await context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<SuProfilGaji>> GetAll()
        //{
        //    return await context.SuProfilGaji.ToListAsync();
        //}

    }
}
