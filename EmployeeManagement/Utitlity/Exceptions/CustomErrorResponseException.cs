using System;

namespace EmployeeManagement.Utitlity.Exceptions
{
    public class CustomErrorResponseException : Exception
    {
        public string ErrorDefinition { get; }
        public int StatusCode { get; }        

        public CustomErrorResponseException(string errorDefinition, string message, int statusCode)
            : base(message)
        {
            ErrorDefinition = errorDefinition;
            StatusCode = statusCode;            
        }
    }
}
