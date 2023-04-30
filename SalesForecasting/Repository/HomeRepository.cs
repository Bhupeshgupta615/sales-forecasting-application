using Dapper;
using SalesForecasting.Data.DataInterface;
using SalesForecasting.IRepository;
using SalesForecasting.Models;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace SalesForecasting.Repository
{
	public class HomeRepository : BaseRepository<HomeViewModel>, IHomeRepository
	{
		private readonly ILogger<HomeRepository> _logger;

        public HomeRepository(ILogger<HomeRepository> logger, IDataBaseFactory dataBaseFactory): base(logger, dataBaseFactory)
        {
            _logger = logger;
        }

        public async Task<List<HomeViewModel>> GetAllState()
        {
            var res = await Datacontext.QueryAsync<HomeViewModel>($"Select Distinct State As States from Orders$", commandType: CommandType.Text);
            return res.ToList();
		}
        public async Task<List<HomeYearModel>> GetYear()
        {
            var res = await Datacontext.QueryAsync<HomeYearModel>($"Select distinct  year(OrderDate) As Year From Orders$", commandType: CommandType.Text);
            return res.ToList();
        }
        public async Task<List<HomeSalesModel>> GetYearSales(int year)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@year", year);

            var res = await Datacontext.QueryAsync<HomeSalesModel>("GetYearSales", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res.ToList();
        }
  
    public async Task<List<HomeSalesModel>> GetForcastedYearSales(int year, string state, int incrementValue)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@year", year);
            parameters.Add("@state", state);
            parameters.Add("@incrementValue", incrementValue);

            var res = await Datacontext.QueryAsync<HomeSalesModel>("GetYearSalesForcast", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res.ToList();
        }

        
    }
}
