using AutoMapper;
using Context;
using DTO;
using Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity
{
    public interface IRoleStore : IRoleStore<RoleDTO, Guid>
    {
    }

    internal class RoleStore : IRoleStore
    {
        private IUnitOfWork _uow;

        public RoleStore(IUnitOfWork uow)
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

        public async Task CreateAsync(RoleDTO role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Add(MapRoleDtoToEntity(role));
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoleDTO role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Update(MapRoleDtoToEntity(role));
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoleDTO role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _uow.RoleRepository.Remove(MapRoleDtoToEntity(role));
            await _uow.SaveChangesAsync();
        }

        public async Task<RoleDTO> FindByIdAsync(Guid roleId)
        {
            ThrowIfDisposed();
            return MapRoleEntityToDto(await _uow.RoleRepository.GetByIdAsync(roleId));
        }

        public async Task<RoleDTO> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var res = (await _uow.RoleRepository.GetAsync(x => x.Name == roleName)).FirstOrDefault();

            return MapRoleEntityToDto(res);
        }

        private Role MapRoleDtoToEntity(RoleDTO roleObj)
        {
            if (roleObj == null) return null;
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<RoleDTO, Role>();
            });
            return Mapper.Map<Role>(roleObj);
        }

        private RoleDTO MapRoleEntityToDto(Role roleObj)
        {
            if (roleObj == null) return null;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Role, RoleDTO>();
            });
            return Mapper.Map<RoleDTO>(roleObj);
        }
    }
}
