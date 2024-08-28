using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProductsAsync();
        Product GetProductByIdAsync(int productId);
        Product CreateProductAsync(CreateProductModel model);
        bool UpdateProductAsync(UpdateProductModel model);
        bool DeleteProductAsync(int productId);
    }
}
