using System.Net;
using static Constants;

namespace EmployeeManagement.Utitlity.Exceptions
{
    public class ForbiddenCustomException : CustomErrorResponseException
    {
        public ForbiddenCustomException() : 
            base(ErrorDefinitions.ForbiddenError, IdentityErrorMessages.Forbidden, (int)HttpStatusCode.Forbidden)
        {
        }
    }
}
