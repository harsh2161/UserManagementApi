using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using UserManagementApi.CustomAttributes;
using UserManagementApi.Models;

namespace UserManagementApi
{
    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(Method = "POST", UriTemplate = "authenticate", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CustomAuthorization(AvailableRoles.All)]
        string AuthenticateUser(UserCredentials userCredentials);

        [WebInvoke(Method = "POST", UriTemplate = "create", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CustomAuthorization(AvailableRoles.Admin)]
        string CreateUser(User newUser);

        [WebInvoke(Method = "PUT", UriTemplate = "update", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CustomAuthorization(AvailableRoles.Admin, AvailableRoles.User)]
        string UpdateUser(User updateUser);

        [WebInvoke(Method = "DELETE", UriTemplate = "delete/{id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CustomAuthorization(AvailableRoles.Admin)]        
        string DeleteUser(string id);  

        [WebInvoke(Method = "POST", UriTemplate = "find/{id}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CustomAuthorization(AvailableRoles.Admin,AvailableRoles.User)]
        List<User> GetUser(SearchFilter searchFilter, string id);
    }
}
