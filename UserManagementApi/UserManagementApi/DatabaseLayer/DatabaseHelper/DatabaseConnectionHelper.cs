using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using System.Data;

namespace UserManagementApi.DatabaseLayer.DatabaseHelper
{
    public class DatabaseConnectionHelper
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DatabaseConnectionHelper));
        public string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        public MySqlConnection Connection;
        public string UserEmail = "";
        public DatabaseConnectionHelper(string userEmail = null)
        {
            ConnectionString = ReadConnectionString();
            Connection = new MySqlConnection(ConnectionString);
            UserEmail = userEmail != null ? userEmail : "";
        }
        public String ReadConnectionString()
        {
            return ConnectionString = "" + ConfigurationManager.AppSettings["ConnectionString"];
        }
        public bool OpenConnection()
        {
            try
            {
                Connection.Open();
                Log.Info(" Succesfully Open Connection Requested by : " + UserEmail);
                return true;
            }
            catch (MySqlException)
            {
                Log.Error(" Unable To Open Connection Requested by : " + UserEmail);
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                Connection.Close();
                Log.Info(" Succesfully Close Connection Requested by : " + UserEmail);
                return true;
            }
            catch (MySqlException)
            {
                Log.Error(" Unable To Close Connection Requested by : " + UserEmail);
                return false;
            }
        }
        public int ExecuteNonQueryCommand(string procedureName, Hashtable parameterList)
        {
            try
            {
                Log.Info("Executing ExecuteNonQuery Request By : " + UserEmail +" with procedure name : " + procedureName);
                if (!OpenConnection())
                {
                    return 0;
                }
                using (MySqlCommand Command = new MySqlCommand(procedureName, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry IterationValue in parameterList)
                    {
                        Command.Parameters.AddWithValue(IterationValue.Key.ToString(), IterationValue.Value);
                    }

                    var Response = Command.ExecuteNonQuery();
                    Log.Info("Returning Response from Database Request By : " + UserEmail + " with procedure name : " + procedureName);
                    return Response;
                }
            }
            finally{
                CloseConnection();
            }
        }
        public MySqlDataReader ExecuteReaderCommand(string procedureName, Hashtable parameterList)
        {            
            if (!OpenConnection())
            {
                return null;
            }
            using (MySqlCommand Command = new MySqlCommand(procedureName, Connection))
            {
                Log.Info("Executing ExecuteReader Request By : " + UserEmail + " with procedure name : " + procedureName);
                Command.CommandType = CommandType.StoredProcedure;

                foreach (DictionaryEntry IterationValue in parameterList)
                {
                    Command.Parameters.AddWithValue(IterationValue.Key.ToString(), IterationValue.Value);
                }

                MySqlDataReader DataReader = Command.ExecuteReader();
                Log.Info("Returning Response from Database Request By : " + UserEmail + " with procedure name : " + procedureName);
                return DataReader;
            }            
        }
        public T ExecuteScalarCommand<T>(string procedureName, Hashtable parameterList)
        {
            try
            {
                Log.Info("Executing Execute scaler Request By : " + UserEmail + " with procedure name : " + procedureName);
                if (!OpenConnection())
                {                    
                    return (T)Convert.ChangeType(null, typeof(string));
                }
                using (MySqlCommand Command = new MySqlCommand(procedureName, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    foreach (DictionaryEntry IterationValue in parameterList)
                    {
                        Command.Parameters.AddWithValue(IterationValue.Key.ToString(), IterationValue.Value);
                    }

                    object DataReader = Command.ExecuteScalar();

                    Log.Info("Returning Response from Database Request By : " + UserEmail + " with procedure name : " + procedureName);
                    return (T)DataReader;
                }
            }
            finally
            {
                CloseConnection();
            }
        }        
    }
}