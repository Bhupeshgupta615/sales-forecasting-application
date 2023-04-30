using SalesForecasting.Controllers;
using SalesForecasting.Data.Data;
using SalesForecasting.Data.DataInterface;
using SalesForecasting.IRepository;
using SalesForecasting.Models;
using SalesForecasting.Repository;
using Scrutor;

namespace SalesForecasting
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataBaseFactory>(x =>
            {
                return new DataBaseFactory(x.GetRequiredService<ILogger<IDataBaseFactory>>(), configuration.GetConnectionString("DBconnection"));


            });
            return services;

        }
		public static IServiceCollection AddCustomAssimblies(this IServiceCollection services)
		{
			var type = new List<Type>()
			{
			  typeof(IDataBaseFactory),
			  typeof(DataBaseFactory),
			  typeof(IHomeRepository),
			  typeof(HomeRepository),
			  typeof(HomeController)

			};
			services.Scan(scan => scan
				.FromAssembliesOf(type)
				.AddClasses()
				.UsingRegistrationStrategy(RegistrationStrategy.Skip)
				.AsMatchingInterface()
				.WithScopedLifetime());
			services.AddTransient<HomeModel>();
			return services;
		}
	}
}
