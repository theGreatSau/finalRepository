using EFProject.Models;

namespace EFProject.Repository
{
	public interface IRoleRepository
	{
		Task<IEnumerable<Role>> GetAllRolesAsync();
		Task<string> GetRoleNameById(int roleId);
		Task<int> InsertRoleAsync(Role role);
		Task<Role?> GetRoleByIdAsync(int roleId);
		Task<PagedResult<Role>> GetAllRoleAsync(string? roleName, int? IsActive, int pageNo, int pageSize);
		Task UpdateRoleAsync(Role role);
		Task DeleteRoleAsync(int roleId, int deletedBy);
	}
}
