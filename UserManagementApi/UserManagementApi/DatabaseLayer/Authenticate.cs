using System;
using System.Collections;
using UserManagementApi.Models;
using UserManagementApi.DatabaseLayer.DatabaseHelper;

namespace UserManagementApi.DatabaseLayer
{
    public class Authenticate
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Authenticate));
        public string AuthenticateUser(string procedureName, Hashtable parameterList, UserCredentials userCredentials)
        {
            Log.Info(" Authentication Process Started for : " + userCredentials.UserEmail);
            DatabaseConnectionHelper DatabaseConnection = new DatabaseConnectionHelper(userCredentials.UserEmail);
            string Response = DatabaseConnection.ExecuteScalarCommand<string>(procedureName, parameterList);

            if(Response == null)
            {
                Log.Info("NotAuthenticated Having Email : " + userCredentials.UserEmail);
                return "NotAuthenticated";
            }

            DateTime Date = DateTime.Now;
            string DateValue = Date.AddDays(2).ToString("yyyy-MM-dd");
            string Token = Helper.TokenService.GenerateToken(userCredentials.UserEmail + "," + Response + "," + userCredentials.UserPassword + "," + DateValue);
            Log.Info("Authenticated User Having Email : " + userCredentials.UserEmail + "With Token Generated : " + Token);
            return Token;
        }
    }
}