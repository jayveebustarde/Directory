using Context.Infrastructure;
using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public interface IUnitOfWork : IDisposable
    {
        BaseRepository<ProductType> ProductTypeRepository { get; set; }
        BaseRepository<Variation> VariationRepository { get; set; }
        BaseRepository<Discount> DiscountRepository { get; set; }
        BaseRepository<ProductDiscount> ProductDiscountRepository { get; set; }
        BaseRepository<Product> ProductRepository { get; set; }

        BaseRepository<User> UserRepository { get; set; }
        BaseRepository<UserClaim> UserClaimRepository { get; set; }
        BaseRepository<Role> RoleRepository { get; set; }
        BaseRepository<UserLogin> UserLoginRepository { get; set; }
        BaseRepository<UserRole> UserRoleRepository { get; set; }

        void SaveChanges(string userId = null);

        Task SaveChangesAsync(string userId = null);
    }

    public class DirectoryUnitOfwork : IUnitOfWork, IDisposable
    {
        private DirectoryContext _context;
        private bool _disposed;

        public DirectoryUnitOfwork()
        {
            _context = new DirectoryContext();
        }

        private BaseRepository<Product> _productRepository;
        private BaseRepository<ProductDiscount> _productDiscountRepository;
        private BaseRepository<Discount> _discountRepository;
        private BaseRepository<Variation> _variationRepository;
        private BaseRepository<ProductType> _productTypeRepository;

        private BaseRepository<User> _userRepository;
        private BaseRepository<UserRole> _userRoleRepository;
        private BaseRepository<Role> _roleRepository;
        private BaseRepository<UserLogin> _userLoginRepository;
        private BaseRepository<UserClaim> _userClaimRepository;

        #region Properties

        public BaseRepository<ProductType> ProductTypeRepository
        {
            get
            {
                if(_productTypeRepository == null)
                {
                    _productTypeRepository = new BaseRepository<ProductType>(_context);
                }
                return _productTypeRepository;
            }
            set { _productTypeRepository = value; }
        }


        public BaseRepository<Variation> VariationRepository
        {
            get
            {
                if(_variationRepository == null)
                {
                    _variationRepository = new BaseRepository<Variation>(_context);
                }
                return _variationRepository;
            }
            set { _variationRepository = value; }
        }


        public BaseRepository<Discount> DiscountRepository
        {
            get
            {
                if(_discountRepository == null)
                {
                    _discountRepository = new BaseRepository<Discount>(_context);
                }
                return _discountRepository;
            }
            set { _discountRepository = value; }
        }


        public BaseRepository<ProductDiscount> ProductDiscountRepository
        {
            get
            {
                if(_productDiscountRepository == null)
                {
                    _productDiscountRepository = new BaseRepository<ProductDiscount>(_context);
                }
                return _productDiscountRepository;
            }
            set { _productDiscountRepository = value; }
        }



        public BaseRepository<Product> ProductRepository
        {
            get
            {
                if(this._productRepository == null)
                {
                    this._productRepository = new BaseRepository<Product>(_context);
                }
                return _productRepository;
            }
            set { _productRepository = value; }
        }

        public BaseRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new BaseRepository<User>(_context);
                }
                return _userRepository;
            }
            set { _userRepository = value; }
        }

        public BaseRepository<UserClaim> UserClaimRepository
        {
            get
            {
                if (this._userClaimRepository == null)
                {
                    this._userClaimRepository = new BaseRepository<UserClaim>(_context);
                }
                return _userClaimRepository;
            }
            set { _userClaimRepository = value; }
        }

        public BaseRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                {
                    this._roleRepository = new BaseRepository<Role>(_context);
                }
                return _roleRepository;
            }
            set { _roleRepository = value; }
        }

        public BaseRepository<UserLogin> UserLoginRepository
        {
            get
            {
                if (this._userLoginRepository == null)
                {
                    this._userLoginRepository = new BaseRepository<UserLogin>(_context);
                }
                return _userLoginRepository;
            }
            set { _userLoginRepository = value; }
        }

        public BaseRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (this._userRoleRepository == null)
                {
                    this._userRoleRepository = new BaseRepository<UserRole>(_context);
                }
                return _userRoleRepository;
            }
            set { _userRoleRepository = value; }
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if(!this._disposed && disposing)
            {
                _context.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void SaveChanges(string user) => _context.SaveChanges(user);

        public Task SaveChangesAsync(string user) => _context.SaveChangesAsync(user);
    }
}
