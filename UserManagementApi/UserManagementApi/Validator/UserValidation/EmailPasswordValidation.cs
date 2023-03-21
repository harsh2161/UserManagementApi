using UserManagementApi.Models;

namespace UserManagementApi.Validator.UserValidation
{
    public static class EmailPasswordValidation
    {
        public static bool IsEmailPasswordValid(UserCredentials userCredentials)
        {
            return (EmailValidation.IsEmailValid(userCredentials.UserEmail) && PasswordValidation.IsPasswordValid(userCredentials.UserPassword));
        }
    }
}