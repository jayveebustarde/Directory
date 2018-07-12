using Context.Infrastructure;
using Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Context.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<bool> IsInRoleAsync(Guid userId, Guid roleId);
    }

    internal class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<bool> IsInRoleAsync(Guid userId, Guid roleId)
        {
            return dbSet.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
