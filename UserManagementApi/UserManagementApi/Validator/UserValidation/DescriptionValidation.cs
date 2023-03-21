using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public class DescriptionValidation
    {
        public static bool IsDescriptionValid(string userDescription)
        {
            if (userDescription == null || userDescription == "")
            {
                throw new ValidationException("Null Description");
            }

            if (userDescription.Length > 1000)
            {
                throw new ValidationException("Description Limit 1000 Words");
            }

            if (userDescription.Contains("<") || userDescription.Contains("/") || userDescription.Contains(">") || userDescription.Contains("\"") || userDescription.Contains(";"))
            {
                throw new ValidationException("Description Prohibited Characters (< , / , > , \" , ;)");
            }
            return true;
        }
    }
}