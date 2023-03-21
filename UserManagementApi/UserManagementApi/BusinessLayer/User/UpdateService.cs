using System;
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
    public class UpdateService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateService));
        private User UpdateUser;
        List<string> UserCredentials = new List<string>();
        public UpdateService(User updateUser)
        {
            UpdateUser = updateUser;

            UserFieldValidation.IsUserFieldValid(updateUser);
            IdValidation.IsIdValid(updateUser.UserID);
            CreationTimeValidation.IsCreationTimeValid(updateUser.UserCreationTime);

            UserCredentials = TokenService.Decrypt(HttpContext.Current.Request.Headers["Token"]).Split(',').ToList();
        }

        public string Update()
        {
            Hashtable ParameterList = ReturnParameters();

            if(UserCredentials[1] == AvailableRoles.Admin.ToString())
            {
                ParameterList.Add("@ID", UpdateUser.UserID);
                UpdateRepository Update = new UpdateRepository(UpdateUser.UserEmail);
                return Update.UpdateUser("UpdateUser", ParameterList);
            }

            if(UserCredentials[1] == AvailableRoles.User.ToString())
            {
                if(UserCredentials[0] != UpdateUser.UserEmail || UpdateUser.UserRole != UserCredentials[1])
                {
                    throw new AuthorizationException("NotAuthorized");
                }
                ParameterList.Add("@ID", "");
                UpdateRepository Update = new UpdateRepository(UpdateUser.UserEmail);
                return Update.UpdateUser("UpdateUser", ParameterList);
            }

            return "NotUpdated";
        }
        private Hashtable ReturnParameters()
        {
            Hashtable ParameterList = new Hashtable();
            ParameterList.Add("@Email", UpdateUser.UserEmail);
            ParameterList.Add("@UserPassword", UpdateUser.UserPassword);
            ParameterList.Add("@UserName", UpdateUser.UserName);
            ParameterList.Add("@Age", UpdateUser.UserAge);            
            ParameterList.Add("@Designation", UpdateUser.UserRole);
            ParameterList.Add("@DateOfBirth", UpdateUser.UserDateOfBirth.ToString("yyyy-MM-dd"));
            ParameterList.Add("@CreationTime", UpdateUser.UserCreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
            ParameterList.Add("@LastModified", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ParameterList.Add("@Salary", UpdateUser.UserSalary);
            ParameterList.Add("@UserDescription", UpdateUser.UserDescription);
            return ParameterList;
        }
    }
}