using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Context;

namespace Services
{
    public class ProductDiscountService : IProductDiscountService
    {
        private DirectoryUnitOfwork _unitOfWork;

        public ProductDiscountService()
        {

        }

        public Guid Create(ProductDiscountDTO objectDTO)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDiscountDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductDiscountDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductDiscountDTO objectDTO)
        {
            throw new NotImplementedException();
        }
    }
}
