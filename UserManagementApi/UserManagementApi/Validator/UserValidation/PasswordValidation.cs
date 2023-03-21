using System.Text.RegularExpressions;
using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public static class PasswordValidation
    {
        public static bool IsPasswordValid(string userPassword)
        {
            Regex ValidateUserPassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            
            if (userPassword == null || userPassword == "")
            {
                throw new ValidationException("Null Password");
            }

            if (userPassword.Length > 30)
            {
                throw new ValidationException("Password Size Limit 30");
            }

            if (!ValidateUserPassword.IsMatch(userPassword))
            {
                throw new ValidationException("Improper Password Format");
            }
                        
            return true;
        }
    }
}