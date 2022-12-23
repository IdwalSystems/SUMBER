using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class JSuKWSPRepository : IRepository<JSuKWSP, int, string>
    {
        public readonly ApplicationDbContext context;

        public JSuKWSPRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JSuKWSP.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<JSuKWSP>> GetAll()
        {
            return await context.JSuKWSP.ToListAsync();
        }

        public async Task<IEnumerable<JSuKWSP>> GetAllIncludeDeletedItems()
        {
            return await context.JSuKWSP
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JSuKWSP> GetById(int id)
        {
            return await context.JSuKWSP.FindAsync(id);
        }

        public async Task<JSuKWSP> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JSuKWSP
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JSuKWSP> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<JSuKWSP> Insert(JSuKWSP entity)
        {
            await context.JSuKWSP.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JSuKWSP entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
