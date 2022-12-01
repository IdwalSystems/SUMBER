using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class JSuKodGajiRepository : IRepository<JSuKodGaji, int, string>
    {
        public readonly ApplicationDbContext context;

        public JSuKodGajiRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JSuKodGaji.FirstOrDefaultAsync(b => b.ID == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }
        public async Task<IEnumerable<JSuKodGaji>> GetAll()
        {
            return await context.JSuKodGaji.ToListAsync();
        }

        public async Task<IEnumerable<JSuKodGaji>> GetAllIncludeDeletedItems()
        {
            return await context.JSuKodGaji
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JSuKodGaji> GetById(int id)
        {
            return await context.JSuKodGaji.FindAsync(id);
        }

        public async Task<JSuKodGaji> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JSuKodGaji
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public Task<JSuKodGaji> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<JSuKodGaji> Insert(JSuKodGaji entity)
        {
            await context.JSuKodGaji.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JSuKodGaji entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
