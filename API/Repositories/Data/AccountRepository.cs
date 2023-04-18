using API.Contexts;
using API.Models;
using API.Repositories.Contracts;
using API.ViewModels;
using System.Transactions;

namespace API.Repositories.Data
{
    public class AcountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        private readonly IUniversityRepository _University;
        private readonly IEducationRepository _Education;
        private readonly IProfilingRepository _profilling;
        private readonly IEmployeeRepository _employee;
        private readonly IAccountRoleRepository _accountRole;

        public AcountRepository(MyContext context,
        IUniversityRepository university,
        IProfilingRepository profiling,
        IEducationRepository educations,
        IEmployeeRepository employee,
        IAccountRoleRepository accountRole
         ) : base(context)
        {

            _University = university;
            _Education = educations;
            _profilling = profiling;
            _employee = employee;
            _accountRole = accountRole;
        }

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getdataEmployee = await _employee.GetAllAsync();
            var GetdataAccount = await GetAllAsync();

            var getdataLogin = getdataEmployee.Join(GetdataAccount,
                             e => e.Nik,
                             a => a.EmployeeNik,
                             (e, a) => new LoginVM
                             {
                                 Email = e.Email,
                                 Password = a.Password,
                             }).FirstOrDefault(ud => ud.Email == loginVM.Email);
            return getdataLogin is not null && loginVM.Password == getdataLogin.Password;
        }

        public async Task<RegisterVM> RegisterAsync(RegisterVM registerVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var university = new University
                {
                    Name = registerVM.UniversityName
                };
                if (await _University.IsNameExist(registerVM.UniversityName))
                {

                }
                else
                {
                    await _University.InsertAsync(university);
                }
                var education = new Education()
                {
                    Degree = registerVM.Degree,
                    Major = registerVM.Major,
                    Gpa = registerVM.GPA,
                    UniversityId = university.Id
                };
                await _Education.InsertAsync(education);

                var employee = new Employee()
                {
                    BirthDate = registerVM.BirthDate,
                    Email = registerVM.Email,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Gender = registerVM.Gender,
                    HiringDate = DateTime.Now,
                    Nik = registerVM.NIK,
                    PhoneNumber = registerVM.PhoneNumber
                };
                await _employee.InsertAsync(employee);
                var profiling = new Profiling()
                {
                    EducationId = education.Id,
                    EmployeeNik = employee.Nik
                };
                await _profilling.InsertAsync(profiling);
                var account = new Account()
                {
                    EmployeeNik = employee.Nik,
                    Password = registerVM.Password
                };
                await InsertAsync(account);

                var accountrole = new AccountRole()
                {
                    AccountNik = account.EmployeeNik,
                    RoleId = 1
                };
                await _accountRole.InsertAsync(accountrole);

                await transaction.CommitAsync();
                return registerVM;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return null;
        }
    }
}