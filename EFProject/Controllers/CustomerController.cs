using EFProject.Common;
using EFProject.Models;
using EFProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFProject.Controllers
{
	public class CustomerController : Controller
	{
		private readonly ICustomerRepository _customerRepository;
		public CustomerController(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}
		[PermissionAuthorize("Customer", "Index")]
		public async Task<IActionResult> Index(string? firstName, string? lastName, string? email, string? mobileNo, int? IsActive, int pageNo = 1)
		{
			int pageSize = 10;
			var customers = await _customerRepository.GetAllCustomerAsync(firstName, lastName, email, mobileNo, IsActive, pageNo, pageSize);
			ViewBag.Message = TempData["Msg"];
			return View(customers);
		}

		[PermissionAuthorize("Customer", "Create")]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Customer req)
		{
			if (!ModelState.IsValid)
			{
				return View(req);
			}
			req.CreatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");
			var check = await _customerRepository.InsertCustomerAsync(req);
			if (check == -1)
			{
				ModelState.AddModelError("Email", "Email already exists.");
				return View(req);
			}
			else if (check == -2)
			{
				ModelState.AddModelError("MobileNo", "Mobile number already exists.");
				return View(req);
			}
			TempData["Msg"] = "Customer created successfully!";
			return RedirectToAction(nameof(Index));
		}

		[PermissionAuthorize("Customer", "Edit")]
		public async Task<IActionResult> Edit(int id)
		{
			var customer = await _customerRepository.GetCustomerByIdAsync(id);
			if (customer == null) return NotFound();

			return View(customer);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Customer req)
		{
			if (!ModelState.IsValid)
			{
				return View(req);
			}

			req.UpdatedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");

			int result = await _customerRepository.UpdateCustomerAsync(req);

			if (result == -1)
			{
				ModelState.AddModelError("Email", "Email is already in use by another customer.");
				return View(req);
			}
			else if (result == -2)
			{
				ModelState.AddModelError("MobileNo", "Mobile number is already in use by another customer.");
				return View(req);
			}

			TempData["Msg"] = "Customer updated successfully!";
			return RedirectToAction(nameof(Index));
		}


		[HttpPost]
		[PermissionAuthorize("Customer", "Delete")]
		public async Task<JsonResult> Delete(int customerId)
		{
			if (customerId <= 0)
			{
				return Json(new { success = false, message = "Invalid CustomerId ID" });
			}

			var deletedBy = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "1");
			await _customerRepository.DeleteCustomerAsync(customerId, deletedBy);
			return Json(new { success = true, message = "Customer deleted successfully!" });
		}

		public async Task<IActionResult> Details(int id)
		{
			CustomerDetails res = new CustomerDetails();
			res.customer = await _customerRepository.GetCustomerByIdAsync(id);
			res.addresses = await _customerRepository.GetCustomerAddressByIdAsync(id);
			return View(res);
		}

	}
}
