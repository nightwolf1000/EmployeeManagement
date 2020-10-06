using System.Net;
using static Constants;

namespace EmployeeManagement.Utitlity.Exceptions
{
    public class ItemNotFoundCustomException : CustomErrorResponseException
    {
        public ItemNotFoundCustomException(int itemId, string message) : 
            base(ErrorDefinitions.NotFoundError, string.Format(message, itemId), (int)HttpStatusCode.NotFound)
        {
        }        
    }
}
