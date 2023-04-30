using SalesForecasting.Models;

namespace SalesForecasting.IRepository
{
	public interface IHomeRepository
	{
		Task<List<HomeViewModel>> GetAllState();
		Task<List<HomeYearModel>> GetYear();
		Task<List<HomeSalesModel>> GetYearSales(int year);
		Task<List<HomeSalesModel>> GetForcastedYearSales(int year,string state,int incrementValue);

	}
}
