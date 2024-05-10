using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMarketApp.repository
{
    public interface IProductRepository
    {        
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<Product> GetProductById(int prId);
        public Task<Product> CreateProduct(Product product);        
        public Task UpdateProduct(int productId, Product product);        
        public Task DeleteProduct(int productId);
    }
    
}