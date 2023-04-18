using API.Contexts;
using API.Models;
using API.Repositories.Contracts;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        private readonly IUniversityRepository _University;
        private readonly IEducationRepository _Education;
        private readonly IProfilingRepository _profilling;
        private readonly IAccountRepository _account;
        private readonly IAccountRoleRepository _accountRole;
        private readonly IRoleRepository _role;
        public EmployeeRepository (MyContext context,
        IUniversityRepository university,
        IProfilingRepository profiling,
        IEducationRepository educations,
        IAccountRepository account,
        IAccountRoleRepository accountRole,
        IRoleRepository role

            ) : base(context)
        {
            _University = university;
            _Education = educations;
            _profilling = profiling;
            _account = account;
            _accountRole = accountRole;
            _role = role;
        }

        public async Task<IEnumerable<EmployeeRole>> GetDataByRoleAsync(String name, int id)
        {
            var getEducation = await _Education.GetAllAsync();
            var getUniversity = await _University.GetAllAsync();
            var getProfilling = await _profilling.GetAllAsync();
            var getemployee = await GetAllAsync();
            var getAccount = await _account.GetAllAsync();
            var getAccountRole = await _accountRole.GetAllAsync();
            var getRole = await _role.GetAllAsync();

            var getData = (from e in getEducation
                           join u in getUniversity
                          on e.UniversityId equals u.Id
                           join p in getProfilling
                           on e.Id equals p.EducationId
                           join em in getemployee
                           on p.EmployeeNik equals em.Nik
                           join ac in getAccount
                           on em.Nik equals ac.EmployeeNik
                           join acr in getAccountRole
                           on ac.EmployeeNik equals acr.AccountNik
                           join r in getRole
                           on acr.RoleId equals r.Id
                           where u.Name == name && acr.RoleId == id
                           orderby e.Gpa descending
                           select new EmployeeRole()
                           {
                               Nik = p.EmployeeNik,
                               Universitas = u.Name,
                               NameEmployee = em.FirstName + " " + em.LastName,
                               Gpa = e.Gpa,
                               AccountRole = r.Name

                           });

            return getData;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return employee is null ? string.Empty : string.Concat(employee.FirstName, " ", employee.LastName);
        }

    }
}
