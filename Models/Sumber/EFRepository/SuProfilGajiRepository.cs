using SUMBER.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using SUMBER.Models.Modules.IRepository;
using Microsoft.EntityFrameworkCore;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class SuProfilGajiRepository : IRepository<SuProfilGaji, int, string>
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

        public async Task<IEnumerable<SuProfilGaji>> GetAll()
        {
            return await context.SuProfilGaji.ToListAsync();
        }

        public async Task<IEnumerable<SuProfilGaji>> GetAllIncludeDeletedItems()
        {
            return await context.SuProfilGaji
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<SuProfilGaji> GetById(int id)
        {
            return await context.SuProfilGaji.FindAsync(id);
        }

        public async Task<SuProfilGaji> GetByIdIncludeDeletedItems(int id)
        {
            return await context.SuProfilGaji
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SuProfilGaji> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

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
            context.Update(entity);
            await context.SaveChangesAsync();
        }

    }
}
