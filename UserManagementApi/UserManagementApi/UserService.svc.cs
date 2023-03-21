using System;
using System.Collections.Generic;
using UserManagementApi.BusinessLayer;
using UserManagementApi.BusinessLayer.UserRepository;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.Models;

namespace UserManagementApi
{
    public class UserService : IUserService
    {        
        public string AuthenticateUser(UserCredentials userCredentials)
        {
            try
            {                
                AuthenticateService AuthenticateService = new AuthenticateService();
                return AuthenticateService.Authenticate(userCredentials);
            }
            catch (Exception exception)
            {
                GlobalException.ThrowError(exception);                
            }
            return "NotAuthenticated";
        }        
        public string CreateUser(User newUser)
        {
            try
            {
                CreateService CreateUser = new CreateService();
                return CreateUser.Add(newUser);
            }
            catch(Exception exception)
            {
                GlobalException.ThrowError(exception);                
            }
            return "UserNotCreated";
        }
        public string UpdateUser(User updateUser)
        {
            try
            {
                UpdateService UpdateService = new UpdateService(updateUser);
                return UpdateService.Update();
            }
            catch (Exception exception)
            {
                GlobalException.ThrowError(exception);
            }
            return "NotUpdated";
        }
        public string DeleteUser(string id)
        {
            try
            {
                DeleteService DeleteService = new DeleteService();
                return DeleteService.Delete(id);
            }
            catch(Exception exception)
            {
                GlobalException.ThrowError(exception);                
            }
            return "UserNotDeleted";
        }
        public List<User> GetUser(SearchFilter searchFilter, string id)
        {
            try
            {
                GetService GetService = new GetService();
                return GetService.Get<User>(searchFilter, id);
            }
            catch(Exception exception)
            {
                GlobalException.ThrowError(exception);
            }
            return new List<User>();
        }
    }
}