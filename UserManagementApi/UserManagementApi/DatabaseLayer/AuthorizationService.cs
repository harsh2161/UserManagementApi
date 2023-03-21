using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementApi.ExceptionHandler;
using UserManagementApi.Helper;
using UserManagementApi.Models;

namespace UserManagementApi.DatabaseLayer
{
    public class AuthorizationService
    {
        private AvailableRoles[] AllowedRole;
        List<string> UserCredentials = new List<string>();
        public AuthorizationService(AvailableRoles[] allowedRole)
        {
            if (HttpContext.Current.Request.Headers["Token"] == null)
            {
                throw new AuthorizationException("Token Null");
            }
            AllowedRole = allowedRole;
            UserCredentials = TokenService.Decrypt(HttpContext.Current.Request.Headers["Token"]).Split(',').ToList();
        }
        public bool AuthorizeUser()
        {
            DateTime Date = DateTime.Parse(UserCredentials[3]);
            if (Date < DateTime.Now)
            {
                throw new AuthorizationException("Token Expired");
            }
            foreach (var role in AllowedRole)
            {
                if (role.ToString() == UserCredentials[1])
                {
                    return true;
                }
            }
            throw new AuthorizationException("NotAuthorized");
        }
    }
}