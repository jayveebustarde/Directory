using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Context;
using Entities;

namespace Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid Create(ProductDTO objectDTO)
        {
            Product product = MapProductDTOToEntity(objectDTO);
            if (product == null) return Guid.Empty;
            try
            {
                using (_unitOfWork)
                {
                    product.Id = Guid.NewGuid();
                    _unitOfWork.ProductRepository.Add(product);
                    _unitOfWork.SaveChanges("Lorem Ipsum");
                }
            }
            catch(Exception e)
            {

            }
            return product.Id;
        }

        public bool Delete(Guid id)
        {
            bool isDeleted = false;
            try
            {
                Product prod = null;
                if (IsProductExisting(id, out prod))
                {
                    using (_unitOfWork)
                    {
                        _unitOfWork.ProductRepository.Remove(prod);
                        _unitOfWork.SaveChanges("Lorem Ipsum");
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isDeleted;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            List<ProductDTO> products = new List<ProductDTO>(); 

            try
            {
                using (_unitOfWork)
                {
                    var productEntities = _unitOfWork.ProductRepository.GetAll();

                    if (productEntities.Any())
                    {
                        foreach (Product product in productEntities)
                        {
                            products.Add(MapProductEntityToDTO(product));
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return products;
        }

        public ProductDTO GetById(Guid id)
        {
            ProductDTO prodObj = new ProductDTO();
            try
            {
                using (_unitOfWork)
                {
                    Product prodData = _unitOfWork.ProductRepository.GetById(id);
                    if(prodData != null)
                    {
                        prodObj = MapProductEntityToDTO(prodData);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return prodObj;
        }

        public bool Update(ProductDTO objectDTO)
        {
            throw new NotImplementedException();
        }

        private ProductDTO MapProductEntityToDTO(Product product)
        {
            if(product != null)
            {
                return new ProductDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name,
                    ProductTypeId = product.ProductTypeId,
                    RegularPrice = product.RegularPrice
                };
            }
            return null;
        }
        
        private Product MapProductDTOToEntity(ProductDTO product)
        {
            if (product != null)
            {
                return new Product
                {
                    Description = product.Description,
                    Name = product.Name,
                    ProductTypeId = product.ProductTypeId,
                    RegularPrice = product.RegularPrice
                };
            }
            return null;
        }

        private bool IsProductExisting(Guid id, out Product prod)
        {
            prod = null;
            try
            {
                using (_unitOfWork)
                {
                    prod = _unitOfWork.ProductRepository.GetById(id);
                    return prod != null;
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}
