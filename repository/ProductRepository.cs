
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


        public async Task<Product> CreateProduct(Product product)
        {
             try
            {
                // MSSQL
                // var query = "INSERT INTO Araclar(aracMusteriKodu,aracPlakaNo,aracHizLimit,aracIPAddress,aracKanalSayisi,aracType,aracGsmIMEI,aracGsmSeriNo,aracUpdateUser,aracUpdateDate) VALUES(@aracMusteriKodu,@aracPlakaNo,@aracHizLimit,@aracIPAddress,@aracKanalSayisi,@aracType,@aracGsmIMEI,@aracGsmSeriNo,@aracUpdateUser,@aracUpdateDate)"
                //      + "SELECT CAST(SCOPE_IDENTITY() as int)";                

                // PSQL
                var query = "INSERT INTO Araclar(aracMusteriKodu,aracPlakaNo,aracHizLimit,aracIPAddress,aracKanalSayisi,aracType,aracGsmIMEI,aracGsmSeriNo,aracUpdateUser,aracUpdateDate) VALUES(@aracMusteriKodu,@aracPlakaNo,@aracHizLimit,@aracIPAddress,@aracKanalSayisi,@aracType,@aracGsmIMEI,@aracGsmSeriNo,@aracUpdateUser,@aracUpdateDate)";
                var parameters = new DynamicParameters();
                
                // parameters.Add("aracMusteriKodu", arac.aracMusteriKodu, DbType.String);
                // parameters.Add("aracPlakaNo", arac.aracPlakaNo, DbType.String);
                // parameters.Add("aracHizLimit", arac.aracHizLimit, DbType.String);                
                // parameters.Add("aracIPAddress", arac.aracIPAddress, DbType.String);
                // parameters.Add("aracKanalSayisi", arac.aracKanalSayisi, DbType.Int32);
                // parameters.Add("aracType", arac.aracType, DbType.String);
                // parameters.Add("aracGsmIMEI", arac.aracGsmIMEI, DbType.String);
                // parameters.Add("aracGsmSeriNo", arac.aracGsmSeriNo, DbType.String);
                // parameters.Add("aracUpdateUser", arac.aracUpdateUser, DbType.String);
                // parameters.Add("aracUpdateDate", arac.aracUpdateDate, DbType.DateTime);

                using (var connection = _context.CreateConnection())
                {
                    int affectedRows  = await connection.ExecuteAsync(query, parameters);

                    if (affectedRows > 0) {
                        var querySelect = "select aracId,aracPlakaNo,aracMusteriKodu,aracIPAddress,aracKanalSayisi,aracType,aracGsmIMEI,aracGsmSeriNo,aracUpdateUser,aracUpdateDate from araclar order by aracid desc limit 1";
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