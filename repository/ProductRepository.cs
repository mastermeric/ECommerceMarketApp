
using System.Data;
using Dapper;
using ECommerceMarketApp.DataContext;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceMarketApp.repository
{        
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context; // Dependency Enjection yapildi ..

        public ProductRepository(DapperContext dapperContext)
        {
            // Dependency Enjection yapildi ..
            _context = dapperContext;
        }


        public async Task<Product> CreateProduct(ProductDto product)
        {
             try
            {
                // MSSQL
                // var query = "INSERT INTO Araclar(aracMusteriKodu,aracPlakaNo,aracHizLimit,aracIPAddress,aracKanalSayisi,aracType,aracGsmIMEI,aracGsmSeriNo,aracUpdateUser,aracUpdateDate) VALUES(@aracMusteriKodu,@aracPlakaNo,@aracHizLimit,@aracIPAddress,@aracKanalSayisi,@aracType,@aracGsmIMEI,@aracGsmSeriNo,@aracUpdateUser,@aracUpdateDate)"
                //      + "SELECT CAST(SCOPE_IDENTITY() as int)";                

                // PSQL
                var query = "INSERT INTO product (prname, prdesc, prprice, prdiscount, prupdatedate) VALUES(@prname, @prdesc, @prprice, @prdiscount, @prupdatedate)";
                var parameters = new DynamicParameters();
                
                parameters.Add("prname", product.prname, DbType.String);
                parameters.Add("prdesc", product.prdesc, DbType.String);
                parameters.Add("prprice", product.prprice, DbType.String);                
                parameters.Add("prdiscount", product.prdiscount, DbType.String);                
                parameters.Add("prupdatedate", product.prupdatedate, DbType.DateTime);

                using (var connection = _context.CreateConnection())
                {
                    int affectedRows  = await connection.ExecuteAsync(query, parameters);

                    if (affectedRows > 0) {
                        var querySelect = "select prid,prname, prdesc, prprice, prdiscount, prupdatedate from product order by prid desc limit 1";
                        Product inserteProduct = await connection.QuerySingleAsync<Product>(querySelect);                        

                        if(inserteProduct != null) {
                            return inserteProduct;
                        } else {
                            return null;
                        }
                    } else {
                        return null;
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);                
            }
        }

        public Task DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
              var query = "SELECT pr.prid,pr.prdesc,pr.prdiscount,pr.prname,pr.prprice,pr.prupdatedate FROM product pr ORDER BY pr.prupdatedate ASC";

            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public Task<Product> GetProductById(int prId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }

    }
}