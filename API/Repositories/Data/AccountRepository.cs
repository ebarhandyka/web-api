using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Profiling, string, MyContext>, IAccountRepository
    {
        public AccountRepository(MyContext context) : base(context) { }
    }
