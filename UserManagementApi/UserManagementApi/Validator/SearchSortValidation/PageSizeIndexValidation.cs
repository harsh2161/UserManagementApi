namespace UserManagementApi.Validator.SearchSortValidation
{
    public class PageSizeIndexValidation
    {
        public static bool IsPageSizeIndexValid(int pageIndex,int pageSize)
        {
            return PageIndexValidation.IsPageIndexValid(pageIndex) && PageSizeValidation.IsPageSizeValid(pageSize);
        }
    }
}