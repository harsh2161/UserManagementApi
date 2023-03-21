using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using UserManagementApi.DatabaseLayer;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.Models;

namespace UserManagementApi.CustomAttributes
{
    public class CustomAuthorization : Attribute, IOperationBehavior, IParameterInspector
    {
        private AvailableRoles[] AllowedRole;
        public CustomAuthorization(params AvailableRoles[] allowedRole)
        {
            AllowedRole = allowedRole;
        }
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            
        }
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            
        }
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            
        }
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }
        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {   
                if(AllowedRole == null)
                {
                    return null;
                } 
                foreach(var role in AllowedRole)
                {
                    if (role == AvailableRoles.All)
                    {
                        return null;
                    }
                }
                AuthorizationService AuthorizationService = new AuthorizationService(AllowedRole);
                AuthorizationService.AuthorizeUser();
            }
            catch(Exception exception)
            {
                GlobalException.ThrowError(exception);
            }
            return null;
        }
        public void Validate(OperationDescription operationDescription)
        {
                  
        }
    }
}


/*
 
     if (AllowedRole == "Admin")
                {
                    if (HttpContext.Current.Request.Headers["Token"] == null)
                    {
                        throw new AuthorizationException("Token Null");
                    }
                    AuthorizationService AuthorizationService = new AuthorizationService(HttpContext.Current.Request.Headers["Token"]);
                    if (AuthorizationService.AuthorizeUser("Admin"))
                    {
                        AllowedRole = "Admin";
                    }
                    else
                    {
                        throw new AuthorizationException("NotAuthorized");
                    }
                }
                else if(AllowedRole == "User")
                {
                    if (HttpContext.Current.Request.Headers["Token"] == null)
                    {
                        throw new AuthorizationException("Token Not Passed Or Validate Or Expired");
                    }
                    AuthorizationService AuthorizationService = new AuthorizationService(HttpContext.Current.Request.Headers["Token"]);
                    if (AuthorizationService.AuthorizeUser("User"))
                    {
                        AllowedRole = "User";
                    }
                    else
                    {
                        throw new AuthorizationException("NotAuthorized");
                    }
                }
     AllowedRole = getRole.ToString();
     */
