using Context.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> FindByNameAsync(string name);
        Task<List<string>> GetRolesNameByUserId(Guid userId);
    }

    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public async Task<Role> FindByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Name.ToUpper() == name.ToUpper());
        }

        public async Task<List<string>> GetRolesNameByUserId(Guid userId)
        {
            var userRoles = db.Set<UserRole>();
            return await (from r in dbSet
                          join ur in userRoles on r.Id equals ur.RoleId
                          where ur.UserId == userId
                          select r.Name).ToListAsync();
        }
    }
}
