using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        Product CreateProduct(CreateProductModel model);
        Product UpdateProduct(UpdateProductModel model);
        bool DeleteProduct(int productId);
    }
}
