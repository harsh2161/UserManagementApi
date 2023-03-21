using System.Collections;
using UserManagementApi.DatabaseLayer.DatabaseHelper;

namespace UserManagementApi.DatabaseLayer.UserRepository
{
    public class UpdateRepository
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateRepository));
        private string UserEmail;
        public UpdateRepository(string userEmail = null)
        {
            UserEmail = userEmail;
        }
        public string UpdateUser(string procedureName, Hashtable parameterList)
        {
            DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
            int Response = DatabaseConnection.ExecuteNonQueryCommand(procedureName, parameterList);

            if (Response != 0)
            {
                Log.Info("User Updated Succesfully with email : " + UserEmail);
                return "SuccesfullyUpdated";
            }

            Log.Info("User Not Updated with email : "+ UserEmail);
            return "NotUpdated";
        }
    }
}