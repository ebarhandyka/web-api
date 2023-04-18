using API.Models;

namespace API.Repositories.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole, int>
    {
        Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
    }
}
