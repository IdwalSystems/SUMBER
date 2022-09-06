using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUMBER.Models.Sumber.EFRepository
{
    public class JSuTarafJawatanRepository : IRepository<JSuTarafJawatan, int, string>
    {
        public readonly ApplicationDbContext context;

        public JSuTarafJawatanRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JSuTarafJawatan.FirstOrDefaultAsync(b => b.ID == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }

        public async Task<IEnumerable<JSuTarafJawatan>> GetAll()
        {
            return await context.JSuTarafJawatan.ToListAsync();
        }

        public async Task<IEnumerable<JSuTarafJawatan>> GetAllIncludeDeletedItems()
        {
            return await context.JSuTarafJawatan
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JSuTarafJawatan> GetById(int id)
        {
            return await context.JSuTarafJawatan.FindAsync(id);
        }

        public async Task<JSuTarafJawatan> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JSuTarafJawatan
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public Task<JSuTarafJawatan> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<JSuTarafJawatan> Insert(JSuTarafJawatan entity)
        {
            await context.JSuTarafJawatan.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JSuTarafJawatan entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
