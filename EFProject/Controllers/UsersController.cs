using EFProject.Common;
using EFProject.Models;
using EFProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFProject.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;

		public UsersController(IUserRepository userRepository,IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		[PermissionAuthorize("Users", "Index")]
		public async Task<IActionResult> Index(string? userName, int? IsActive ,int pageNo = 1)
		{
			int pageSize = 10;
			var users = await _userRepository.GetAllUsersAsync(userName, IsActive, pageNo, pageSize);
			ViewBag.Message = TempData["Msg"];
			return View(users);
		}

		[PermissionAuthorize("Users", "Create")]
		public async Task<IActionResult> Create()
		{
			ViewBag.Roles = await _roleRepository.GetAllRolesAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(User user)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = await _roleRepository.GetAllRolesAsync();
				return View(user);
			}

			user.CreatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");

			await _userRepository.InsertUserAsync(user);
			TempData["Msg"] = "User created successfully!";
			return RedirectToAction(nameof(Index));
		}

		[PermissionAuthorize("Users", "Edit")]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);
			if (user == null) return NotFound();

			ViewBag.Roles = await _roleRepository.GetAllRolesAsync();
			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(User user)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = await _roleRepository.GetAllRolesAsync();
				return View(user);
			}

			user.UpdatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");

			await _userRepository.UpdateUserAsync(user);
			TempData["Msg"] = "User updated successfully!";
			return RedirectToAction(nameof(Index));
		}
		
		[HttpPost]
		[PermissionAuthorize("Users", "Delete")]
		public async Task<JsonResult> Delete(int userId)
		{
			if (userId <= 0)
			{
				return Json(new { success = false, message = "Invalid User ID" });
			}

			var deletedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");
			await _userRepository.DeleteUserAsync(userId, deletedBy);
			return Json(new { success = true, message = "User deleted successfully!" });
		}

	}
}
