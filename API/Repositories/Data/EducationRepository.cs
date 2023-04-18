using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data
{
    public class EducationRepository : GeneralRepository<Education, int, MyContext>, IEducationRepository
    {
        public EducationRepository(MyContext context) : base(context) { }
    }

}
