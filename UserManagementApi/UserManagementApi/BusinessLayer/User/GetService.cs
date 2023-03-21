using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementApi.BusinessLayer.BusinessHelper;
using UserManagementApi.DatabaseLayer.UserRepository;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.Helper;
using UserManagementApi.Models;
using UserManagementApi.Validator.UserValidation;

namespace UserManagementApi.BusinessLayer.UserRepository
{
    public class GetService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(GetService));
        private List<string> UserCredentials = new List<string>();
        private Hashtable ParameterList = new Hashtable();
        public GetService()
        {
            UserCredentials = TokenService.Decrypt(HttpContext.Current.Request.Headers["Token"]).Split(',').ToList();            
        }       
        public List<T> Get<T>(SearchFilter searchFilter, string id)
        {
            ParameterList.Clear();
            if (UserCredentials[1] == AvailableRoles.Admin.ToString() && id == "all")
            {
                ParameterList = SearchSortHashTable.GetSearchSortHashTable(searchFilter);
                return (List<T>)Convert.ChangeType(Execute<T>("SearchFilterSorts"), typeof(List<T>));
            }
            else if (UserCredentials[1] == AvailableRoles.Admin.ToString())
            {
                IdValidation.IsIdValid(id);
                ParameterList.Add("@ID", id);
                return (List<T>)Convert.ChangeType(Execute<T>("ReteriveUser"), typeof(List<T>));
            }
            else if (UserCredentials[1] == AvailableRoles.User.ToString())
            {
                ParameterList.Add("@Email", UserCredentials[0]);
                ParameterList.Add("@UserPassword", UserCredentials[2]);
                return (List<T>)Convert.ChangeType(Execute<T>("ReterieveByEmailPassword"), typeof(List<T>));
            }
            throw new AuthorizationException("Access Denied");
        }
        private List<T> Execute<T>(string procedureName)
        {
            GetRepository GetData = new GetRepository();
            return (List<T>)Convert.ChangeType(GetData.Get<List<T>>(procedureName, ParameterList), typeof(List<T>));
        }       
    }
}