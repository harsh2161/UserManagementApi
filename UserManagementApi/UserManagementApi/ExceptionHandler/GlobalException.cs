using System;
using System.Net;
using System.ServiceModel.Web;

namespace UserManagementApi.ExceptionHandler
{
    public class GlobalException
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(GlobalException));
        public static void ThrowError(Exception exception)
        {
            HttpStatusCode GetStatusCode;
            if(exception is AuthorizationException)
            {
                Log.Error("Throwing Unauthorized exception " + exception);
                GetStatusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is ValidationException)
            {
                Log.Error("Throwing Validation exception" + exception);
                GetStatusCode = HttpStatusCode.BadRequest;
            }
            else 
            {
                Log.Error("Throwing Exception " + exception);
                throw new WebFaultException<String>("Oops ! Something Went Wrong", HttpStatusCode.InternalServerError);
            }
            Log.Error("Throwing WebFault Exception " + exception);
            throw new WebFaultException<string>(exception.Message, GetStatusCode);
        }
    }
}