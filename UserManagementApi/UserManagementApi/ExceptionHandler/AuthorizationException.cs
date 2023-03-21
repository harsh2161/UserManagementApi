using System;

namespace UserManagementApi.ExceptionHandler
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(): base() { }
        public AuthorizationException(string message, Exception InnerException) : base(message) { }
        public AuthorizationException(string message): base(message) { }
    }
}