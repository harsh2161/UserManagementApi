using MySql.Data.MySqlClient;
using System.Collections;
using UserManagementApi.DatabaseLayer.DatabaseHelper;

namespace UserManagementApi.DatabaseLayer.UserRepository
{
    public class GetRepository
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(GetRepository));
        DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
        public T Get<T>(string procedureName, Hashtable parameterList)
        {
            try
            {                
                MySqlDataReader Response = DatabaseConnection.ExecuteReaderCommand(procedureName, parameterList);
                ReadService ReadService = new ReadService();

                Log.Info("Getting Response of ExecuteReader for"+procedureName);

                return ReadService.Read<T>(Response);
            }
            finally
            {
                DatabaseConnection.CloseConnection();
            }
        }
    }
}