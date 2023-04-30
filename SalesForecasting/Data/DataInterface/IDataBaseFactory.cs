using System.Data;

namespace SalesForecasting.Data.DataInterface
{
    public interface IDataBaseFactory : IDisposable
    {
        IDbConnection Get();
    }
}
