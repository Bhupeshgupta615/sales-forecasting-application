using Microsoft.Extensions.Logging;
using SalesForecasting.Data.DataInterface;
using System.Data;
using System.Data.SqlClient;

namespace SalesForecasting.Data.Data
{
    public class DataBaseFactory : Disposable, IDataBaseFactory
    {
        private readonly ILogger<IDataBaseFactory> _logger;
        private readonly string _connectionString;



        public DataBaseFactory(ILogger<IDataBaseFactory> logger, string connectionstring)
        {
              _logger= logger;
              _connectionString= connectionstring;
        }

        private IDbConnection _dbcontext;

        public IDbConnection Get()
        {
            if(_dbcontext == null)
            {
                try
                {
                    _dbcontext = new SqlConnection(_connectionString);
                }
                catch (Exception ex) 
                {
                    _logger.LogError("Error In ConnectionString");
                }
            }
            return _dbcontext;
        }
        protected override void DisposeCore()
        {
            if (_dbcontext != null)
                _dbcontext.Dispose();
        }
    }
}