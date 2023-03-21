using System.Collections;
using UserManagementApi.DatabaseLayer;
using UserManagementApi.Models;
using UserManagementApi.Validator.UserValidation;

namespace UserManagementApi.BusinessLayer
{
    public class AuthenticateService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AuthenticateService));
        public string Authenticate(UserCredentials userCredentials)
        {
            EmailPasswordValidation.IsEmailPasswordValid(userCredentials);
            
            Hashtable ParameterList = ReturnParameters(userCredentials);
            
            Authenticate Authenticate = new Authenticate();
            return Authenticate.AuthenticateUser("authenticate", ParameterList, userCredentials);
        }

        private Hashtable ReturnParameters(UserCredentials userCredentials)
        {
            Hashtable ParameterList = new Hashtable();
            ParameterList.Add("@Email", userCredentials.UserEmail);
            ParameterList.Add("@UserPassword", userCredentials.UserPassword);

            return ParameterList;
        }
    }
}