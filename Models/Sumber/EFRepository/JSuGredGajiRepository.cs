using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class JSuGredGajiRepository : IRepository<JSuGredGaji, int, string>

    {
        public readonly ApplicationDbContext context;

        public JSuGredGajiRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JSuGredGaji.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<JSuGredGaji>> GetAll()
        {
            return await context.JSuGredGaji.ToListAsync();
        }

        public async Task<IEnumerable<JSuGredGaji>> GetAllIncludeDeletedItems()
        {
            return await context.JSuGredGaji
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JSuGredGaji> GetById(int id)
        {
            return await context.JSuGredGaji.FindAsync(id);
        }

        public async Task<JSuGredGaji> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JSuGredGaji
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JSuGredGaji> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<JSuGredGaji> Insert(JSuGredGaji entity)
        {
            await context.JSuGredGaji.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JSuGredGaji entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
