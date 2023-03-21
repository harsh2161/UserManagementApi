using System.Collections;
using UserManagementApi.DatabaseLayer.DatabaseHelper;

namespace UserManagementApi.DatabaseLayer.UserRepository
{
    public class DeleteRepository
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DeleteRepository));
        private string Id;
        public DeleteRepository(string id = null)
        {
            Id = id;
        }
        public string DeleteUser(string procedureName, Hashtable parameterList)
        {
            DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
            int Response = DatabaseConnection.ExecuteNonQueryCommand(procedureName, parameterList);

            if (Response != 0)
            {
                Log.Info(" User Deleted Succesfully Having Id : " + Id);
                return "SuccesfullyDeleted";
            }
            Log.Info("User Not Deleted Having Id : " + Id);
            return "UserNotDeleted";
        }
    }
}