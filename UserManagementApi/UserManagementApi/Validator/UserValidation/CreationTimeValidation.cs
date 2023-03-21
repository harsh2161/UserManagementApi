using System;
using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public class CreationTimeValidation
    {
        public static bool IsCreationTimeValid(DateTime DateOfBirth)
        {
            if (DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                throw new ValidationException("Creation Time Improper Format");
            }
            if (DateOfBirth > DateTime.Now)
            {
                throw new ValidationException("Creation Time Should be Less Equal To Today");
            }
            return true;
        }
    }
}