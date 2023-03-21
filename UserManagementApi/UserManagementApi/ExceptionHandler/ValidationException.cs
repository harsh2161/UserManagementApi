using System;

namespace UserManagementApi.ExceptionHandler
{
    public class ValidationException : Exception
    {
        public ValidationException(): base() { }
        public ValidationException(string message, Exception InnerException) : base(message) { }
        public ValidationException(string message): base(message) { }
    }
}