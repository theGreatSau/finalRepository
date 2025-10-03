namespace EFProject.Models
{
	public class PagedResult<T>
	{
		public List<T> Items { get; set; } = new List<T>();
		public int PageNo { get; set; }
		public int PageSize { get; set; }
		public int TotalItems { get; set; }

		public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

		public bool HasPreviousPage => PageNo > 1;
		public bool HasNextPage => PageNo < TotalPages;
	}
}
