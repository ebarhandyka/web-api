using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        public EmployeeRepository(MyContext context) : base(context) { }
    }
}
