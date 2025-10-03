using EFProject.Models;

namespace EFProject.Repository
{
	public interface IUserRepository
	{
		Task<User?> AuthenticateAsync(string username, string password);

		Task<int> InsertUserAsync(User user);
		Task<User?> GetUserByIdAsync(int userId);
		Task<PagedResult<User>> GetAllUsersAsync(string? userName, int? IsActive, int pageNo, int pageSize);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int userId, int deletedBy);
	}
}
