using EFProject.Repository;
using EFProject.Services;

namespace EFProject.Common
{
	public static class ServiceCollectionExtensions
	{
		public static void RegisterRepositories(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config.GetConnectionString("DefaultConnection");

			services.AddScoped<IUserRepository>(sp => new UserServices(connectionString));
			services.AddScoped<IRoleRepository>(sp => new RoleServices(connectionString));
			services.AddScoped<ICustomerRepository>(sp => new CustomerService(connectionString));
		}
	}
}
