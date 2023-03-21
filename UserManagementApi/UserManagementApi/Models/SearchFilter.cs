namespace UserManagementApi.Models
{
    public class SearchFilter
    {
        public string SearchText { get; set; }
        public User Filter { get; set; }
        public string SortBy { get; set; }
        public enum Sort { Ascending, Descending }
        public Sort SortDirection { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public SearchFilter()
        {
            PageIndex = 0;
            PageSize = 7;
        }
    }
}