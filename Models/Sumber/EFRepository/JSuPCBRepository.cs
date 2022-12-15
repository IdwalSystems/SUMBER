using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Modules.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SUMBER.Models.Sumber.EFRepository
{
    public class JSuPCBRepository : IRepository<JSuPCB, int, string>
    {
        public readonly ApplicationDbContext context;

        public JSuPCBRepository(ApplicationDbContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var model = await context.JSuPCB.FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                context.Remove(model);
            }
        }
        public async Task<IEnumerable<JSuPCB>> GetAll()
        {
            return await context.JSuPCB.ToListAsync();
        }

        public async Task<IEnumerable<JSuPCB>> GetAllIncludeDeletedItems()
        {
            return await context.JSuPCB
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JSuPCB> GetById(int id)
        {
            return await context.JSuPCB.FindAsync(id);
        }

        public async Task<JSuPCB> GetByIdIncludeDeletedItems(int id)
        {
            return await context.JSuPCB
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<JSuPCB> GetByString(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<JSuPCB> Insert(JSuPCB entity)
        {
            await context.JSuPCB.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(JSuPCB entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }

}

