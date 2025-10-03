using EFProject.Models;

namespace EFProject.Repository
{
	public interface ICustomerRepository
	{
		Task<PagedResult<Customer>> GetAllCustomerAsync(string? firstName, string? lastName, string? email, string? mobileNo, int? IsActive, int pageNo, int pageSize);
		Task<int> InsertCustomerAsync(Customer req);
		Task<int> UpdateCustomerAsync(Customer req);
		Task DeleteCustomerAsync(int customerId, int deletedBy);
		Task<Customer?> GetCustomerByIdAsync(int customerId);
		Task<List<CustomerAddress>> GetCustomerAddressByIdAsync(int customerId);
	}
}
