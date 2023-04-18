using API.Models;
using API.ViewModels;

namespace API.Repositories.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, string>
    {
        Task<RegisterVM> RegisterAsync(RegisterVM registerVM);
        Task<bool> LoginAsync(LoginVM loginVM);
    }
}
