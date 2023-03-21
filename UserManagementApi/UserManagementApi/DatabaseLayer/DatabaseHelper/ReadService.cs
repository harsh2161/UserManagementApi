using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UserManagementApi.Models;

namespace UserManagementApi.DatabaseLayer.DatabaseHelper
{
    public class ReadService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ReadService));
        public T Read<T>(MySqlDataReader response)
        {
            try
            {
                Type GetType = typeof(T);
                Log.Info("Checking Type of generic");
                if (GetType == typeof(List<User>))
                {
                    return (T)Convert.ChangeType(ReadUserList(response), typeof(T));
                }
                return (T)Convert.ChangeType(null, typeof(string));
            }
            finally
            {
                DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper();
                DatabaseConnection.CloseConnection();
            }
        }
        private List<User> ReadUserList(MySqlDataReader response)
        {
            if (!response.HasRows)
            {
                Log.Info("Response from database is null");
                return new List<User>();
            }
            List<User> Users = new List<User>();
            while (response.Read())
            {
                User NewUser = new User();
                NewUser.UserEmail = response["Email"] + "";
                NewUser.UserPassword = response["UserPassword"] + "";
                NewUser.UserName = response["UserName"] + "";
                NewUser.UserAge = int.Parse(response["Age"] + "");
                NewUser.UserID = response["ID"] + "";
                NewUser.UserRole = response["Designation"] + "";
                NewUser.UserDateOfBirth = Convert.ToDateTime(response["DateOfBirth"] + "");
                NewUser.UserCreationTime = Convert.ToDateTime(response["CreationTime"] + "");
                NewUser.UserLastModified = Convert.ToDateTime(response["LastModified"] + "");
                NewUser.UserSalary = int.Parse(response["Salary"] + "");
                NewUser.UserDescription = response["UserDescription"] + "";
                Users.Add(NewUser);
            }
            Log.Info("Returning List of Users");
            return Users;
        }       
    }
}