using API.Contexts;
using API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey>
        where TEntity : class
        where TContext : MyContext
    {
        protected TContext _context;
        public GeneralRepository(TContext context)
        {
            _context = context;
        }
        public async Task<TEntity> DeleteAsync(TKey key)
        {
            var entity = await GetByIdAsync(key);
            _context.Set<TEntity>().Remove(entity!);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity>? GetByIdAsync(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }


        public virtual async Task<TEntity?> InsertAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

       

        public async Task<TEntity> UpdateAsync(TEntity Entity)
        {
            _context.Set<TEntity>().Update(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
    }
}
