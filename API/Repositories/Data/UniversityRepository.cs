﻿using API.Contexts;
using API.Models;
using API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, int, MyContext>, IUniversityRepository
    {
        public UniversityRepository(MyContext context) : base(context)
        {

        }

        public async Task<University?> GetByNameAsync(string name)
        {
            return await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> IsNameExist(string name)
        {
            var entity = await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
            return entity != null;
        }
        public override async Task<University?> InsertAsync(University entity)
        {
            if (await IsNameExist(entity.Name))
            {
                return await GetByNameAsync(entity.Name);
            }
            return await base.InsertAsync(entity);
        }
    }
}
