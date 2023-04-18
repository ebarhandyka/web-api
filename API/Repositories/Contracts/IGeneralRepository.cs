using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Repositories.Contracts
{
    public interface IGeneralRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey key);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity Entity);
        Task<TEntity> DeleteAsync(TKey key);
    }

}
