using EFProject.Models;
using EFProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using EFProject.Common;

namespace EFProject.Controllers
{
	public class RoleController : Controller
	{
		private readonly IRoleRepository _roleRepository;
		private readonly string _connectionString = "Data Source=ACCELERATEBSI;Initial Catalog=EFDatabase;Integrated Security=False;User ID=sa;Password=Abs@123;Encrypt=False;";

		public RoleController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		[PermissionAuthorize("Role", "Index")]
		public async Task<IActionResult> Index(string? roleName, int? IsActive, int pageNo = 1)
		{
			int pageSize = 10;
			var roles = await _roleRepository.GetAllRoleAsync(roleName, IsActive, pageNo, pageSize);
			ViewBag.Message = TempData["Msg"];
			return View("~/Views/Role/Index.cshtml",roles);
		}

		[HttpGet]
		public string GetRoleName(int roleId)
		{
			var roleName = _roleRepository.GetRoleNameById(roleId);

			return roleName.Result ?? "Not found";
		}

		[PermissionAuthorize("Role", "Create")]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Role role)
		{
			if (!ModelState.IsValid)
			{
				return View(role);
			}

			role.CreatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");

			await _roleRepository.InsertRoleAsync(role);
			TempData["Msg"] = "Role created successfully!";
			return RedirectToAction(nameof(Index));
		}

		[PermissionAuthorize("Role", "Edit")]
		public async Task<IActionResult> Edit(int id)
		{
			var role = await _roleRepository.GetRoleByIdAsync(id);
			if (role == null) return NotFound();

			return View(role);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Role role)
		{
			if (!ModelState.IsValid)
			{
				return View(role);
			}

			role.UpdatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");

			await _roleRepository.UpdateRoleAsync(role);
			TempData["Msg"] = "Role updated successfully!";
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[PermissionAuthorize("Role", "Delete")]
		public async Task<JsonResult> Delete(int roleId)
		{
			if (roleId <= 0)
			{
				return Json(new { success = false, message = "Invalid Role ID" });
			}

			var deletedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");
			await _roleRepository.DeleteRoleAsync(roleId, deletedBy);
			return Json(new { success = true, message = "Role deleted successfully!" });
		}

		[PermissionAuthorize("Role", "AssignPermissions")]
		public async Task<IActionResult> AssignPermissions(int id)
		{
			using var connection = new SqlConnection(_connectionString);

			var allPermissions = await connection.QueryAsync<Permission>("SELECT * FROM Permission");
			var assigned = await connection.QueryAsync<int>(
				"SELECT PermID FROM RolePermissions WHERE RoleId = @RoleId", new { RoleId = id });

			var model = new RolePermissionViewModel
			{
				RoleId = id,
				Permissions = allPermissions.Select(p => new PermissionCheckBox
				{
					PermID = p.PermID,
					Name = $"{p.Controller}--{p.Name}",
					IsAssigned = assigned.Contains(p.PermID)
				}).ToList()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AssignPermissions(int roleId, List<int> selectedPermissions)
		{
			using var connection = new SqlConnection(_connectionString);

			await connection.ExecuteAsync("DELETE FROM RolePermissions WHERE RoleId = @RoleId", new { RoleId = roleId });

			foreach (var permId in selectedPermissions)
			{
				await connection.ExecuteAsync("INSERT INTO RolePermissions (RoleId, PermID) VALUES (@RoleId, @PermID)",
					new { RoleId = roleId, PermID = permId });
			}

			return RedirectToAction("AssignPermissions", new { roleId });
		}
	}
}
