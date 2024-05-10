using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMarketApp.DAL
{
    public class Dapper
    {
        public class Dappers: IDapper
        {
            private readonly IConfiguration _config;
            private string Connectionstring = "DefaultConnection";

            public Dappers(IConfiguration config)
            {
                _config = config;
            }
            public void Dispose()
            {
                //test after publicToprivate
            }

            public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
            {
                throw new NotImplementedException();
            }

            public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
            {
                using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            }

            public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
            {
                using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
                return db.Query<T>(sp, parms, commandType: commandType).ToList();
            }

            public DbConnection GetDbconnection()
            {
                return new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
            }

            public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
            {
                T result;
                using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using var tran = db.BeginTransaction();
                    try
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

                return result;
            }

            public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
            {
                T result;
                using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using var tran = db.BeginTransaction();
                    try
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

                return result;
            }
        }

    }
}
