using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data
{
    public class RoleRepository : GeneralRepository<Role, int, MyContext>, IRoleRepository
    {
        public RoleRepository(MyContext context) : base(context) { }
    }
}
