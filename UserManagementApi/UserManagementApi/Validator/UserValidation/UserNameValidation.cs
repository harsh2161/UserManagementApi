using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public static class UserNameValidation
    {
        public static bool IsUserNameValid(string userName)
        {
            if (userName == null || userName == "")
            {
                throw new ValidationException("Null UserName");
            }

            if (userName.Length < 3 || userName.Length > 100)
            {
                throw new ValidationException("UserName Should Be Minimum 3 to 100 Max Length");
            }

            if (userName.Contains("<") || userName.Contains("/") || userName.Contains(">") || userName.Contains("\"") || userName.Contains(";"))
            {
                throw new ValidationException("UserName Prohibited Characters (< , / , > , \" , ;)");
            }
            return true;
        }
    }
}