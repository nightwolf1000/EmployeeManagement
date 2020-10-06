using System.Net;
using static Constants;

namespace EmployeeManagement.Utitlity.Exceptions
{
    public class BadRequestCustomException : CustomErrorResponseException
    {
        public BadRequestCustomException(string message) :
            base(ErrorDefinitions.BadRequestError, message, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
