using Context.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Context.Repositories
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<UserClaim>> GetByUserId(Guid userId);
    }

    internal class UserClaimRepository : BaseRepository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<List<UserClaim>> GetByUserId(Guid userId)
        {
            return dbSet.Where(uc => uc.UserId == userId).ToListAsync();
        }
    }
}
