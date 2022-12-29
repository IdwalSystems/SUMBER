using SUMBER.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using SUMBER.Models.Modules.IRepository;
using Microsoft.EntityFrameworkCore;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class SuGajiRepository : IRepository<SuGaji, int, string>
    {

        public readonly ApplicationDbContext context;

        public SuGajiRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.SuGaji.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<SuGaji>> GetAll()
        {
            return await context.SuGaji.ToListAsync();
        }

        public async Task<IEnumerable<SuGaji>> GetAllIncludeDeletedItems()
        {
            return await context.SuGaji
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<SuGaji> GetById(int id)
        {
            return await context.SuGaji.FindAsync(id);
        }

        public async Task<SuGaji> GetByIdIncludeDeletedItems(int id)
        {
            return await context.SuGaji
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<SuGaji> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SuGaji> Insert(SuGaji entity)
        {
            await context.SuGaji.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(SuGaji entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

    }
}
