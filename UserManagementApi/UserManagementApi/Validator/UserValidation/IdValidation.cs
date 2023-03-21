using System.Linq;
using UserManagementApi.ExceptionHandler;

namespace UserManagementApi.Validator.UserValidation
{
    public static class IdValidation
    {
        public static bool IsIdValid(string id)
        {
            string SpecialCharacterNotAllowed = @"\|!#$%&/()=?»«@£§€{}.;'<>_,";

            if (id == null || id == "")
            {
                throw new ValidationException("Id Null");
            }

            if (id.Length < 10 || id.Length > 100)
            {
                throw new ValidationException("Id Length Range 10 to 100");
            }

            foreach (var character in SpecialCharacterNotAllowed)
            {
                if (id.Contains(character))
                {
                    throw new ValidationException("Prohibited Characters (\\|!#$%&/()=?»«@£§€{}.;'<>_,)");
                }
            }
            return true;
        }
    }
}