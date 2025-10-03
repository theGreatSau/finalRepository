using Dapper;
using EFProject.Models;
using EFProject.Repository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EFProject.Services
{
	public class CustomerService : ICustomerRepository
	{
		private readonly string _connectionString;
		public CustomerService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task DeleteCustomerAsync(int customerId, int deletedBy)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			await db.ExecuteAsync(
				"sp_DeleteCustomer",
				new { CustomerId = customerId, DeletedBy = deletedBy },
				commandType: CommandType.StoredProcedure);
		}

		public async Task<PagedResult<Customer>> GetAllCustomerAsync(string? firstName, string? lastName, string? email, string? mobileNo, int? IsActive, int pageNo, int pageSize)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("FirstName", firstName);
				parameters.Add("@LastName", lastName);
				parameters.Add("@email", email);
				parameters.Add("@MobileNo", mobileNo);
				parameters.Add("@IsActive", IsActive);
				parameters.Add("@PageNo", pageNo);
				parameters.Add("@PageSize", pageSize);

				// Execute the stored procedure to get both users and total record count
				var CustomerWithCount = await db.QueryAsync<dynamic>(
					"sp_GetAllCustomer",
					parameters,
					commandType: CommandType.StoredProcedure
				);

				var customers = CustomerWithCount.Select(x => new Customer
				{
					CustomerId = x.CustomerId,
					FirstName = x.FirstName,
					LastName = x.LastName,
					email = x.email,
					MobileNo = x.MobileNo,
					Password = x.Password,
					IsActive = x.IsActive,
					CreatedByName = x.CreatedByName,
					CreatedOn = x.CreatedOn,
					UpdatedByName = x.UpdatedByName,
					UpdatedOn = x.UpdatedOn,
				}).ToList();

				int totalRecords = CustomerWithCount.FirstOrDefault()?.TotalRecords ?? 0;

				// Return the result wrapped in a PagedResult object
				return new PagedResult<Customer>
				{
					Items = customers,
					PageNo = pageNo,
					PageSize = pageSize,
					TotalItems = totalRecords
				};
			}
		}
		public async Task<List<CustomerAddress>> GetCustomerAddressByIdAsync(int customerId)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var customerAddresses = await db.QueryAsync<CustomerAddress>(
				"GetCustomerDetails",
				new { CustomerId = customerId },
				commandType: CommandType.StoredProcedure);

			return customerAddresses.ToList();
		}

		public async Task<Customer?> GetCustomerByIdAsync(int customerId)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var customer = await db.QueryFirstOrDefaultAsync<Customer>(
				"sp_GetCustomerById",
				new { CustomerId = customerId },
				commandType: CommandType.StoredProcedure);

			return customer;	
		}

		public async Task<int> InsertCustomerAsync(Customer req)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@FirstName", req.FirstName);
			parameters.Add("@LastName", req.LastName);
			parameters.Add("@email", req.email);
			parameters.Add("@MobileNo", req.MobileNo);
			parameters.Add("@Password", req.Password);
			parameters.Add("@IsActive", req.IsActive);
			parameters.Add("@CreatedBy", req.CreatedBy);

			var newCustomerId = await db.ExecuteScalarAsync<int>("sp_InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
			return newCustomerId;
		}

		public async Task<int> UpdateCustomerAsync(Customer req)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@CustomerId", req.CustomerId);
			parameters.Add("@FirstName", req.FirstName);
			parameters.Add("@LastName", req.LastName);
			parameters.Add("@Email", req.email);  // Fix casing to match stored proc
			parameters.Add("@MobileNo", req.MobileNo);
			parameters.Add("@Password", req.Password);
			parameters.Add("@IsActive", req.IsActive);
			parameters.Add("@UpdatedBy", req.UpdatedBy);

			int result = await db.ExecuteScalarAsync<int>(
				"sp_UpdateCustomer",
				parameters,
				commandType: CommandType.StoredProcedure
			);

			return result;
		}

	}
}
