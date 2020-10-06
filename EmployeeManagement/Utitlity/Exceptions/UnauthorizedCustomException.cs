using System.Net;
using static Constants;

namespace EmployeeManagement.Utitlity.Exceptions
{
    public class UnauthorizedCustomException : CustomErrorResponseException
    {
        public UnauthorizedCustomException() :
            base(ErrorDefinitions.UnauthorizedError, IdentityErrorMessages.Unathorized, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
