using System.Collections;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.DatabaseLayer.DatabaseHelper;

namespace UserManagementApi.DatabaseLayer.UserRepository
{
    public class CreateRepository
    {
        private string UserEmail;
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CreateRepository));
        public CreateRepository(string userEmail = null)
        {            
            UserEmail = userEmail != null ? userEmail : "";

            if (!CheckForDuplicate())
            {
                throw new ValidationException("This Email Already Exist");
            }
        }
        private bool CheckForDuplicate()
        {
            Hashtable ParameterList = new Hashtable();
            ParameterList.Add("@Email", UserEmail);

            DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
            string Response = DatabaseConnection.ExecuteScalarCommand<string>("CheckDuplicate", ParameterList);

            if (Response!=null)
            {
                return false;
            }
            return true;
        }
        public string CreateUser(string procedureName, Hashtable parameterList)
        {
            Log.Info(" Creating The User With Email : " + UserEmail);
            DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
            int Response = DatabaseConnection.ExecuteNonQueryCommand(procedureName, parameterList);

            if (Response != 0)
            {
                Log.Info(" Created Succesfully The User With Email : "+UserEmail);
                return "SuccesfullyCreated";
            }
            
            Log.Info(" Unable To Create The User With Email : " + UserEmail);
            return "UserNotCreated";
        }
    }
}