using EFProject.Models;
using EFProject.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace EFProject.Services
{
	public class UserServices : IUserRepository
	{
		private readonly string _connectionString;

		public UserServices(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<User?> AuthenticateAsync(string username, string password)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var sql = @"
                SELECT TOP 1 UserId, Username, Password, RoleId, IsDeleted
                FROM Users
                WHERE Username = @Username AND Password = @Password AND IsDeleted = 0";

			var user = await db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username, Password = password });
			return user;
		}

		public async Task<int> InsertUserAsync(User user)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@Username", user.Username);
			parameters.Add("@Password", user.Password);
			parameters.Add("@RoleId", user.RoleId);
			parameters.Add("@IsActive", user.IsActive);
			parameters.Add("@CreatedBy", user.CreatedBy);

			var newUserId = await db.ExecuteScalarAsync<int>("sp_InsertUser", parameters, commandType: CommandType.StoredProcedure);
			return newUserId;
		}

		public async Task<User?> GetUserByIdAsync(int userId)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var user = await db.QueryFirstOrDefaultAsync<User>(
				"sp_GetUserById",
				new { UserId = userId },
				commandType: CommandType.StoredProcedure);

			return user;
		}

		public async Task<PagedResult<User>> GetAllUsersAsync(string? userName,int? IsActive, int pageNo, int pageSize)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("@UserName", userName);
				parameters.Add("@IsActive", IsActive);
				parameters.Add("@PageNo", pageNo);
				parameters.Add("@PageSize", pageSize);

				// Execute the stored procedure to get both users and total record count
				var usersWithCount = await db.QueryAsync<dynamic>(
					"sp_GetAllUsers",
					parameters,
					commandType: CommandType.StoredProcedure
				);

				var users = usersWithCount.Select(x => new User
				{
					UserId = x.UserId,
					Username = x.UserName,
					RoleId = x.RoleId,
					RoleName = x.RoleName,
					IsActive = x.IsActive,
					CreatedByName = x.CreatedByName,
					CreatedOn = x.CreatedOn,
					UpdatedByName = x.UpdatedByName,
					UpdatedOn = x.UpdatedOn,
				}).ToList();

				int totalRecords = usersWithCount.FirstOrDefault()?.TotalRecords ?? 0;

				// Return the result wrapped in a PagedResult object
				return new PagedResult<User>
				{
					Items = users,
					PageNo = pageNo,
					PageSize = pageSize,
					TotalItems = totalRecords
				};
			}
		}


		public async Task UpdateUserAsync(User user)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			var parameters = new DynamicParameters();
			parameters.Add("@UserId", user.UserId);
			parameters.Add("@Username", user.Username);
			parameters.Add("@Password", user.Password);
			parameters.Add("@RoleId", user.RoleId);
			parameters.Add("@IsActive", user.IsActive);
			parameters.Add("@UpdatedBy", user.UpdatedBy);

			await db.ExecuteAsync("sp_UpdateUser", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task DeleteUserAsync(int userId, int deletedBy)
		{
			using IDbConnection db = new SqlConnection(_connectionString);

			await db.ExecuteAsync(
				"sp_DeleteUser",
				new { UserId = userId, DeletedBy = deletedBy },
				commandType: CommandType.StoredProcedure);
		}
	}
}
