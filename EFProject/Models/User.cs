namespace EFProject.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Username { get; set; } = "";
		public string Password { get; set; } = "";
		public int RoleId { get; set; }
		public string? RoleName { get; set; }
		public int IsDeleted { get; set; }
		public int IsActive { get; set; }
		public int CreatedBy { get; set; }
		public string? CreatedByName { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int UpdatedBy { get; set; }
		public string? UpdatedByName { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}

	public class UserReq
	{
		public string Username { get; set; }
	}
}
