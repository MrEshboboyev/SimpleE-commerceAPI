using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class ProductService : IProductService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public Product CreateProduct(CreateProductModel model)
        {
            try
            {
                var product = new Product
                {
                    Category = model.Category,
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    StockQuantity = model.StockQuantity
                };

                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                Product? objFromDb = _unitOfWork.Product.Get(p => p.ProductId == productId);
                _unitOfWork.Product.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Product.GetAll();
        }

        public Product GetProductById(int productId)
        {
            return _unitOfWork.Product.Get(p => p.ProductId == productId);
        }

        public Product UpdateProduct(UpdateProductModel model)
        {
            try
            {
                Product? objFromDb = _unitOfWork.Product.Get(p => p.ProductId == model.ProductId);

                // update product fields
                objFromDb.Category = model.Category;
                objFromDb.Description = model.Description;
                objFromDb.Name = model.Name;
                objFromDb.Price = model.Price;
                objFromDb.StockQuantity = model.StockQuantity;

                _unitOfWork.Product.Update(objFromDb);
                _unitOfWork.Save();
                return objFromDb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
