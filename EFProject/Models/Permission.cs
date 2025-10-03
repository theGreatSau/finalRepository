namespace EFProject.Models
{
	public class Permission
	{
		public int PermID { get; set; }
		public string Name { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
	}

	public class RolePermission
	{
		public int RoleId { get; set; }
		public int PermID { get; set; }
	}

	public class RolePermissionViewModel
	{
		public int RoleId { get; set; }
		public List<PermissionCheckBox> Permissions { get; set; }
	}

	public class PermissionCheckBox
	{
		public int PermID { get; set; }
		public string Name { get; set; }
		public bool IsAssigned { get; set; }
	}

}
