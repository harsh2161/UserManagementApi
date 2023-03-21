using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public class SalaryValidation
    {
        public static bool IsSalaryValid(int salary)
        {
            if (salary < 0)
            {
                throw new ValidationException("Salary Cant Be Negative");
            }
            return true;
        }
    }
}