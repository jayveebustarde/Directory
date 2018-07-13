using AutoMapper;
using Context;
using DTO;
using Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity
{
    public interface IUserStore : IUserLoginStore<UserDTO, Guid>,
        IUserClaimStore<UserDTO, Guid>,
        IUserRoleStore<UserDTO, Guid>,
        IUserPasswordStore<UserDTO, Guid>,
        IUserSecurityStampStore<UserDTO, Guid>,
        IUserEmailStore<UserDTO, Guid>,
        IUserPhoneNumberStore<UserDTO, Guid>,
        IUserTwoFactorStore<UserDTO, Guid>,
        IUserLockoutStore<UserDTO, Guid>
    {

    }

    internal class UserStore : IUserStore
    {
        private IUnitOfWork _uow;

        public UserStore(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && _uow != null)
            {
                if (disposing)
                {
                    _uow = null;
                }
                _disposed = true;
            }
        }

        #region user

        public async Task CreateAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            _uow.UserRepository.Add(MapObjects<User>(user));
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            _uow.UserRepository.Update(MapObjects<User>(user));
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            _uow.UserRepository.Remove(MapObjects<User>(user));
            await _uow.SaveChangesAsync();
        }

        public async Task<UserDTO> FindByIdAsync(Guid userId)
        {
            ThrowIfDisposed();
            return MapObjects<UserDTO>(await _uow.UserRepository.GetByIdAsync(userId));
        }

        public async Task<UserDTO> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            return MapObjects<UserDTO>((await _uow.UserRepository.GetAsync(x => x.UserName == userName)).FirstOrDefault());
        }

        #endregion

        #region userlogin

        public async Task AddLoginAsync(UserDTO user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            _uow.UserLoginRepository.Add(new UserLogin { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, UserId = user.Id });
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveLoginAsync(UserDTO user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            _uow.UserLoginRepository.Remove(l => l.UserId == user.Id && l.LoginProvider == login.LoginProvider && l.UserId == user.Id);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            var logins = await _uow.UserLoginRepository.GetAsync(x => x.UserId == user.Id);
            return logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
        }

        public async Task<UserDTO> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
                throw new ArgumentNullException("login");

            var userLogin = await _uow.UserLoginRepository.GetAsync(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            if (userLogin == null)
                return default(UserDTO);

            return MapObjects<UserDTO>(await _uow.UserRepository.GetByIdAsync((userLogin).FirstOrDefault().UserId));
        }

        #endregion

        #region claims

        public async Task<IList<Claim>> GetClaimsAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            var claims = await _uow.UserClaimRepository.GetAsync(x => x.UserId == user.Id);

            return claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(UserDTO user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            _uow.UserClaimRepository.Add(new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveClaimAsync(UserDTO user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            _uow.UserClaimRepository.Remove(c => c.UserId == user.Id && c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
            await _uow.SaveChangesAsync();
        }

        #endregion

        #region roles

        public async Task AddToRoleAsync(UserDTO user, string roleName)
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var role = (await _uow.RoleRepository.GetAsync(x => x.Name == roleName)).FirstOrDefault();
            if (role == null)
                throw new InvalidOperationException("role not found");

            _uow.UserRoleRepository.Add(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            });
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveFromRoleAsync(UserDTO user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var role = (await _uow.RoleRepository.GetAsync(x => x.Name == roleName)).FirstOrDefault();
            if (role == null)
                throw new InvalidOperationException("role not found");

            _uow.UserRoleRepository.Remove(r => r.UserId == user.Id && r.RoleId == role.Id);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            return (IList<string>)(await _uow.UserRoleRepository.GetAsync(x => x.UserId == user.Id)).Select(x => x.Role.Name);
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var role = (await _uow.RoleRepository.GetAsync(x => x.Name == roleName)).FirstOrDefault();
            if (role == null)
                throw new InvalidOperationException("role not found");

            return (await _uow.UserRoleRepository.GetAsync(x => x.UserId == user.Id && x.RoleId == role.Id)).Any();
        }

        #endregion

        public async Task SetPasswordHashAsync(UserDTO user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.PasswordHash = passwordHash;
            await _uow.SaveChangesAsync();
        }

        public Task<string> GetPasswordHashAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserDTO user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task SetSecurityStampAsync(UserDTO user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.SecurityStamp = stamp;
            await _uow.SaveChangesAsync();
        }

        public Task<string> GetSecurityStampAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public async Task SetEmailAsync(UserDTO user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.Email = email;
            await _uow.SaveChangesAsync();
        }

        public Task<string> GetEmailAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.EmailConfirmed);
        }

        public async Task SetEmailConfirmedAsync(UserDTO user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.EmailConfirmed = confirmed;

            await _uow.SaveChangesAsync();
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return MapObjects<UserDTO>((await _uow.UserRepository.GetAsync(x => x.Email == email)).FirstOrDefault());
        }

        public async Task SetPhoneNumberAsync(UserDTO user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.PhoneNumber = phoneNumber;

            await _uow.SaveChangesAsync();
        }

        public Task<string> GetPhoneNumberAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public async Task SetPhoneNumberConfirmedAsync(UserDTO user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.PhoneNumberConfirmed = confirmed;

            await _uow.SaveChangesAsync();
        }

        public async Task SetTwoFactorEnabledAsync(UserDTO user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.TwoFactorEnabled = enabled;

            await _uow.SaveChangesAsync();
        }

        public Task<bool> GetTwoFactorEnabledAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : new DateTimeOffset());
        }

        public async Task SetLockoutEndDateAsync(UserDTO user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;

            await _uow.SaveChangesAsync();
        }

        public async Task<int> IncrementAccessFailedCountAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            ++user.AccessFailedCount;

            await _uow.SaveChangesAsync();

            return user.AccessFailedCount;
        }

        public async Task ResetAccessFailedCountAsync(UserDTO user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.AccessFailedCount = 0;

            await _uow.SaveChangesAsync();
        }

        public Task<int> GetAccessFailedCountAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEnabled);
        }

        public async Task SetLockoutEnabledAsync(UserDTO user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.LockoutEnabled = enabled;
            await _uow.SaveChangesAsync();
        }

        private T MapObjects<T>(object userObject)
            where T : class
        {
            if (userObject == null) return null;
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, T>();
                cfg.CreateMap<UserDTO, T>();
                cfg.CreateMap<UserLogin, T>();
                cfg.CreateMap<UserLoginDTO, T>();
                cfg.CreateMap<UserClaim, T>();
                cfg.CreateMap<UserClaimDTO, T>();
                cfg.CreateMap<UserRole, T>();
                cfg.CreateMap<UserRoleDTO, T>();
                cfg.CreateMap<Role, T>();
                cfg.CreateMap<RoleDTO, T>();
            });
            if(userObject is User)
            {
                return Mapper.Map<T>((User)userObject);
            }
            if (userObject is UserDTO)
            {
                return Mapper.Map<T>((UserDTO)userObject);
            }
            if (userObject is UserLogin)
            {
                return Mapper.Map<T>((UserLogin)userObject);
            }
            if (userObject is UserLoginDTO)
            {
                return Mapper.Map<T>((UserLoginDTO)userObject);
            }
            if (userObject is UserClaim)
            {
                return Mapper.Map<T>((UserClaim)userObject);
            }
            if (userObject is UserClaimDTO)
            {
                return Mapper.Map<T>((UserClaimDTO)userObject);
            }
            if (userObject is UserRole)
            {
                return Mapper.Map<T>((UserRole)userObject);
            }
            if (userObject is UserRoleDTO)
            {
                return Mapper.Map<T>((UserRoleDTO)userObject);
            }
            if (userObject is Role)
            {
                return Mapper.Map<T>((Role)userObject);
            }
            if (userObject is RoleDTO)
            {
                return Mapper.Map<T>((RoleDTO)userObject);
            }
            return null;
        }
    }
}
