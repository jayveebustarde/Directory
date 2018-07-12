using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Context.Infrastructure;
using Entities;

namespace Context.Repositories
{
    public interface IUserLoginRepository : IRepository<UserLogin>
    {
        Task<List<UserLogin>> GetByUserId(Guid userId);
        Task<UserLogin> FindByLoginProviderAndProviderKey(string loginProvider, string providerKey);
    }

    internal class UserLoginRepository : BaseRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<List<UserLogin>> GetByUserId(Guid userId)
        {
            return dbSet.Where(ul => ul.UserId == userId).ToListAsync();
        }

        public Task<UserLogin> FindByLoginProviderAndProviderKey(string loginProvider, string providerKey)
        {
            return dbSet.FirstOrDefaultAsync(ul => ul.LoginProvider == loginProvider && ul.ProviderKey == providerKey);
        }
    }
}
