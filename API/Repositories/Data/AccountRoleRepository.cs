using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRoleRepository
    {
        private readonly IRoleRepository _role;
        public AccountRoleRepository(MyContext context, IRoleRepository role) : base(context)
        {
            _role = role;
        }

        public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
        {
            var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.AccountNik == nik);
            var getRole = await _role.GetAllAsync();

            var getRoleByNik = from ar in getAccountRoleByAccountNik
                               join r in getRole on ar.RoleId equals r.Id
                               select r.Name;

            return getRoleByNik;
        }
    }
}
