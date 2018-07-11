using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
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

        public int SaveChanges(string user) => _context.SaveChanges(user);

        public Task<int> SaveChangesAsync(string user) => _context.SaveChangesAsync(user);
    }
}
