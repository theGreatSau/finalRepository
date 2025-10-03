using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.Metrics;

namespace EFProject.Models
{
	public class Customer
	{
		public int MyProperty { get; set; }
		public int CustomerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string email { get; set; }
		public string MobileNo { get; set; }
		public string Password { get; set; }
		public int IsActive { get; set; }
		public int IsDeleted { get; set; }
		public int CreatedBy { get; set; }
		public string? CreatedByName { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int UpdatedBy { get; set; }
		public string? UpdatedByName { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}

	public class CustomerAddress
	{
		public int AddressId { get; set; }
		public int CustomerId { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string PinCode { get; set; }
		public int IsPrimary { get; set; }
		public int AddressType { get; set; }
		public int CreatedBy { get; set; }
		public string? CreatedByName { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int UpdatedBy { get; set; }
		public string? UpdatedByName { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}

	public class CustomerDetails
	{
		public Customer customer { get; set; }
		public List<CustomerAddress> addresses { get; set; }
	}
}
