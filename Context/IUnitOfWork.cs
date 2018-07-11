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

        int SaveChanges(string userId);

        Task<int> SaveChangesAsync(string userId);
    }
}
