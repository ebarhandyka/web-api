using API.Models;

namespace API.Repositories.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
        Task<bool> IsNameExist(string name);
        Task<University?> GetByNameAsync(string name);
    }

}
