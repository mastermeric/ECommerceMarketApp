using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMarketApp.repository
{
    public interface IProductRepository
    {        
        public Task<IEnumerable<ProductDtoGet>> GetAllProducts();
        public Task<ProductDtoGet> GetProductById(int prId);
        public Task<Product> CreateProduct(ProductDtoPost product);        
        public Task UpdateProduct(int productId, Product product);        
        public Task DeleteProduct(int productId);
    }
    
}