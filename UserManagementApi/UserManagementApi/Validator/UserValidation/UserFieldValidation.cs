using UserManagementApi.Models;

namespace UserManagementApi.Validator.UserValidation
{
    public static class UserFieldValidation
    {
        public static bool IsUserFieldValid(User user)
        {
            EmailValidation.IsEmailValid(user.UserEmail);

            PasswordValidation.IsPasswordValid(user.UserPassword);

            UserNameValidation.IsUserNameValid(user.UserName);

            AgeValidation.IsAgeValid(user.UserAge);

            SalaryValidation.IsSalaryValid(user.UserSalary);

            DescriptionValidation.IsDescriptionValid(user.UserDescription);

            DateTimeValidation.IsDateTimeValid(user.UserDateOfBirth);

            return true;
        }
    }
}