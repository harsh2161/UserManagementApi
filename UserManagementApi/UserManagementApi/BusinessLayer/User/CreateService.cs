using System;
using System.Collections;
using UserManagementApi.DatabaseLayer.UserRepository;
using UserManagementApi.Models;
using UserManagementApi.Validator.UserValidation;

namespace UserManagementApi.BusinessLayer.UserRepository
{
    public class CreateService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CreateService));
        public string Add(User newUser)
        {
            UserFieldValidation.IsUserFieldValid(newUser);

            Log.Info(" All Fields Are Validated for User With : " + newUser.UserEmail);
                                    
            Hashtable ParameterList = ReturnParameters(newUser);

            CreateRepository Create = new CreateRepository(newUser.UserEmail);
            return Create.CreateUser("CreateUser", ParameterList);                        
        }

        private Hashtable ReturnParameters(User newUser)
        {
            Guid UserID = Guid.NewGuid();
            Hashtable ParameterList = new Hashtable();
            ParameterList.Add("@Email", newUser.UserEmail);
            ParameterList.Add("@UserPassword", newUser.UserPassword);
            ParameterList.Add("@UserName", newUser.UserName);
            ParameterList.Add("@Age", newUser.UserAge);
            ParameterList.Add("@ID", UserID.ToString());
            ParameterList.Add("@Designation", newUser.UserRole);
            ParameterList.Add("@DateOfBirth", newUser.UserDateOfBirth.ToString("yyyy-MM-dd"));
            ParameterList.Add("@CreationTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ParameterList.Add("@LastModified", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ParameterList.Add("@Salary", newUser.UserSalary);
            ParameterList.Add("@UserDescription", newUser.UserDescription);

            return ParameterList;
        }
    }
}