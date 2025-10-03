using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Dapper;

namespace EFProject.Common
{
	public class PermissionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
	{
		private readonly string _controller;
		private readonly string _action;

		public PermissionAuthorizeAttribute(string controller, string action)
		{
			_controller = controller;
			_action = action;
		}

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var user = context.HttpContext.User;

			if (!user.Identity.IsAuthenticated)
			{
				context.Result = new RedirectToActionResult("Login", "Account", null);
				return;
			}

			var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			if (roleClaim == null)
			{
				context.Result = new ForbidResult();
				return;
			}

			var roleId = int.Parse(roleClaim.Value);

			using var connection = new SqlConnection("Data Source=ACCELERATEBSI;Initial Catalog=EFDatabase;Integrated Security=False;User ID=sa;Password=Abs@123;Encrypt=False;");

			var sql = @"
            SELECT COUNT(*) FROM RolePermissions rp
            INNER JOIN Permission p ON rp.PermID = p.PermID
            WHERE rp.RoleId = @RoleId AND p.Controller = @Controller AND p.Action = @Action
        ";

			var hasPermission = await connection.ExecuteScalarAsync<int>(sql, new
			{
				RoleId = roleId,
				Controller = _controller,
				Action = _action
			});

			if (hasPermission == 0)
			{
				context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
			}
		}
	}
}
