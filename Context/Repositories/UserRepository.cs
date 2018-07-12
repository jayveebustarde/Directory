using System.Data.Entity;
using System.Threading.Tasks;
using Context.Infrastructure;
using Entities;

namespace Context.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByNameAsync(string name);
        Task<User> FindByEmailAsync(string email);
    }

    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.UserName.ToUpper() == name.ToUpper());
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }
    }
}
