using API.Models;
using API.ViewModels;

namespace API.Repositories.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {

        Task<string> GetFullNameByEmailAsync(string email);

        Task<IEnumerable<EmployeeRole>> GetDataByRoleAsync(String name, int id);
    }
}
