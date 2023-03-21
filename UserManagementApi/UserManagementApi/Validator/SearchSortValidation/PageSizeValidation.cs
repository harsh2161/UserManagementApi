using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.SearchSortValidation
{
    public class PageSizeValidation
    {
        public static bool IsPageSizeValid(int pageSize)
        {
            if (pageSize <= 0 || pageSize > 50)
            {
                throw new ValidationException("Page Size Should Less Than 50");
            }
            return true;
        }
    }
}