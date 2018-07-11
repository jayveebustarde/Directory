using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using DTO;
using Entities;

namespace Services
{
    public class ProductTypeService : IProductTypeService
    {
        private IUnitOfWork _unitOfWork;

        public ProductTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid Create(ProductTypeDTO objectDTO)
        {
            ProductType productType = new ProductType { Type = objectDTO.Type };
            if (productType == null) return Guid.Empty;
            try
            {
                using (_unitOfWork)
                {
                    productType.Id = Guid.NewGuid();
                    _unitOfWork.ProductTypeRepository.Insert(productType);
                    _unitOfWork.SaveChanges("Lorem Ipsum");
                }
            }
            catch (Exception e)
            {

            }
            return productType.Id;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductTypeDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductTypeDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductTypeDTO objectDTO)
        {
            throw new NotImplementedException();
        }
    }
}
