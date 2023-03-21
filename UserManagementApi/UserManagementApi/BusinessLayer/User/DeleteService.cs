using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementApi.DatabaseLayer.UserRepository;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.Helper;
using UserManagementApi.Models;
using UserManagementApi.Validator.UserValidation;

namespace UserManagementApi.BusinessLayer.UserRepository
{
    public class DeleteService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DeleteService));
        private List<string> UserCredentials = new List<string>();
        public DeleteService()
        {
            UserCredentials = TokenService.Decrypt(HttpContext.Current.Request.Headers["Token"]).Split(',').ToList();
        }
        public string Delete(string id)
        {
            IdValidation.IsIdValid(id);            
            IsUserValid(id);
            Hashtable ParameterList = ReturnParameters(id);
            DeleteRepository Delete = new DeleteRepository(id);
            return Delete.DeleteUser("DeleteUser", ParameterList);
        }
        private void IsUserValid(string id)
        {
            GetService GetService = new GetService();
            List<User> User = GetService.Get<User>(null, id);
            if(UserCredentials[0] == User[0].UserEmail)
            {
                throw new ValidationException("You Can't Delete Yourself");
            }
        }
        private Hashtable ReturnParameters(string id)
        {
            Hashtable ParameterList = new Hashtable();
            ParameterList.Add("@ID", id);
            return ParameterList;
        }
    }
}
