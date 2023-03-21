using System;
using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public class DateTimeValidation
    {
        public static bool IsDateTimeValid(DateTime DateOfBirth)
        {           
            if (DateOfBirth.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                throw new ValidationException("Date Time Improper Format");
            }
            DateOfBirth = DateOfBirth.AddDays(1);

            if (DateOfBirth > DateTime.Now)
            {
                throw new ValidationException("DateOfBirth Should be Less Than Today");
            }
            return true;
        }
    }
}