using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.SearchSortValidation
{
    public static class PageIndexValidation
    {
        public static bool IsPageIndexValid(int pageIndex)
        {
            if (pageIndex < 0)
            {
                throw new ValidationException("Page Index Should Not Less Than 0");
            }
            return true;
        }
    }
}