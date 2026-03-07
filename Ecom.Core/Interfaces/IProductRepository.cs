using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // for future method
        Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);
    }
}
  