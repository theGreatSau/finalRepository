using EFProject.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using EFProject.Repository;

namespace EFProject.Services
{
	public class RoleServices : IRoleRepository
	{
		private readonly string _connectionString;

		public RoleServices(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task DeleteRoleAsync(int roleId, int deletedBy)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			await db.ExecuteAsync(
				"sp_DeleteRole",
				new { RoleId = roleId, DeletedBy = deletedBy },
				commandType: CommandType.StoredProcedure);
		}

		public async Task<PagedResult<Role>> GetAllRoleAsync(string? roleName, int? IsActive, int pageNo, int pageSize)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("@RoleName", roleName);
				parameters.Add("@IsActive", IsActive);
				parameters.Add("@PageNo", pageNo);
				parameters.Add("@PageSize", pageSize);

				// Execute the stored procedure to get both users and total record count
				var rolsWithCount = await db.QueryAsync<dynamic>(
					"sp_GetAllRoles",
					parameters,
					commandType: CommandType.StoredProcedure
				);

				var roles = rolsWithCount.Select(x => new Role
				{
					RoleId = x.RoleId,
					RoleName = x.RoleName,
					IsActive = x.IsActive,
					CreatedByName = x.CreatedByName,
					CreatedOn = x.CreatedOn,
					UpdatedByName = x.UpdatedByName,
					UpdatedOn = x.UpdatedOn,
				}).ToList();

				int totalRecords = rolsWithCount.FirstOrDefault()?.TotalRecords ?? 0;

				// Return the result wrapped in a PagedResult object
				return new PagedResult<Role>
				{
					Items = roles,
					PageNo = pageNo,
					PageSize = pageSize,
					TotalItems = totalRecords
				};
			}
		}

		public async Task<IEnumerable<Role>> GetAllRolesAsync()
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var roles = await db.QueryAsync<Role>(
				"GetAllRoles",
				commandType: CommandType.StoredProcedure);

			return roles;
		}

		public async Task<Role?> GetRoleByIdAsync(int roleId)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var role = await db.QueryFirstOrDefaultAsync<Role>(
				"sp_GetRoleById",
				new { RoleId = roleId },
				commandType: CommandType.StoredProcedure);

			return role;
		}

		public async Task<string> GetRoleNameById(int roleId)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@RoleId", roleId);

			var RoleName = await db.QueryAsync<string>("GetRoleName", parameters,commandType: CommandType.StoredProcedure);
			return RoleName.FirstOrDefault();
		}

		public async Task<int> InsertRoleAsync(Role role)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@@Rolename", role.RoleName);
			parameters.Add("@IsActive", role.IsActive);
			parameters.Add("@CreatedBy", role.CreatedBy);

			var newRoleId = await db.ExecuteScalarAsync<int>("sp_InsertRole", parameters, commandType: CommandType.StoredProcedure);
			return newRoleId;
		}

		public async Task UpdateRoleAsync(Role role)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@RoleId", role.RoleId);
			parameters.Add("@RoleName", role.RoleName);
			parameters.Add("@IsActive", role.IsActive);
			parameters.Add("@UpdatedBy", role.UpdatedBy);

			await db.ExecuteAsync("sp_UpdateRole", parameters, commandType: CommandType.StoredProcedure);
		}
	}
}
