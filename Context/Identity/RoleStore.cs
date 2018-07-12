using System;
using System.Collections.Generic;
using System.Linq;
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

        public RoleStore(IUnitOfWork uow, IRoleRepository roleRepository)
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
                    _uow.RoleRepository = null;
                }
                _disposed = true;
            }
        }

        public async Task CreateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Add(role);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Update(role);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Remove(role);
            await _uow.SaveChangesAsync();
        }

        public async Task<Role> FindByIdAsync(Guid roleId)
        {
            ThrowIfDisposed();
            return await _uow.RoleRepository.GetByIdAsync(roleId);
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var res = (await _uow.RoleRepository.GetAsync(x => x.Name == roleName)).FirstOrDefault();

            return res;
        }
    }
}
