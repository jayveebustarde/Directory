using System;
using System.Threading.Tasks;
using Context.Infrastructure;
using Context.Repositories;
using Entities;
using Microsoft.AspNet.Identity;

namespace Context.Identity
{
    public interface IRoleStore : IRoleStore<Role, Guid>
    {
    }

    internal class RoleStore : IRoleStore
    {
        private IUnitOfWork _uow;
        private IRoleRepository _roleRepository;

        public RoleStore(IUnitOfWork uow, IRoleRepository roleRepository)
        {
            _uow = uow;
            _roleRepository = roleRepository;
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
                    _roleRepository = null;
                }
                _disposed = true;
            }
        }

        public async Task CreateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Add(role);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Update(role);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Remove(role);
            await _uow.SaveChangesAsync();
        }

        public async Task<Role> FindByIdAsync(Guid roleId)
        {
            ThrowIfDisposed();
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            return await _roleRepository.FindByNameAsync(roleName);
        }
    }
}
