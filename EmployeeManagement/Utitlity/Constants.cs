public static class Constants
{
    public static class ViewHeadings
    {
        public static readonly string Employees = "Employees";
        public static readonly string Assignments = "Assignments";
        public static readonly string AddAnEmployee = "Add an Employee";
        public static readonly string EditAnEmployee = "Edit an Employee";
        public static readonly string AddAnAssignment = "Add an assignment";
        public static readonly string EditAnAssignment = "Edit an assignment";
        public static readonly string EmployeeDetails = "Employee Details";
    }

    public static class UploadsSettings
    {
        public static readonly int MaxFileSize = 1 * 1024 * 1024;
        public static readonly string[] AcceptedFileTypes = { ".jpg", ".jpeg", ".png" };
    }

    public static class RoleNames
    {
        public const string Administrator = "Administrator";
    }

    public static class MapperItemsKeys
    {
        public static readonly string SearchTerm = "SearchTerm";
        public static readonly string SelectedEmployees = "SelectedEmployees";
        public static readonly string SelectedAssignments = "SelectedAssignments";
    }

    public static class ErrorDefinitions
    {
        public static readonly string BadRequestError = "400 Bad Request Error";
        public static readonly string UnauthorizedError = "401 Unauthorized Error";
        public static readonly string ForbiddenError = "403 Forbidden Error";
        public static readonly string NotFoundError = "404 Not Found Error";
        public static readonly string InternalServerError = "500 Internal Server Error";
    }

    public static class UploadErrorMessages
    {
        public static readonly string FileCannotBeNull = "Uploaded file cannot be null";
        public static readonly string FileCannotBeEmpty = "Uploaded file cannot be empty";
        public static readonly string MaxFileSizeExceeded = $"Max file size {UploadsSettings.MaxFileSize} exceeded";
        public static readonly string InvalidFileType = $"Invalid file type. Accepted types: {string.Join(", ", UploadsSettings.AcceptedFileTypes)}";
    }

    public static class InvalidDateMessages
    {
        public static readonly string InvalidDateFormat = "Invalid date format";
        public static readonly string BirthDateCannotBeAFutureDate = "Birth date cannot be a future date";
        public static readonly string EmployeeMustBe18orOver = "Employee must be 18 or over";
        public static readonly string HireDateCannotBeAFutureDate = "Hire date cannot be a future date";
        public static readonly string HireDateCannotBeBeforeBirthDate = "Hire date cannot be before the birth date";
        public static readonly string HireDateMustBe18orMoreYearsAfterBirthDate = "Hire date must be 18 years or more after the birth date";
    }

    public static class IdentityErrorMessages
    {
        public static readonly string Unathorized = "Authentication is required to access this resource";
        public static readonly string Forbidden = "You do not have permission to access this resource";
    }

    public static class NotFoundMessages
    {      
        public static readonly string ResourceCannotBeFound = "Sorry, the resource you requested could not be found";
        public static readonly string EmployeeCannotBeFound = "Employee with ID = {0} cannot be found";
        public static readonly string AssignmentCannotBeFound = "Assignment with ID = {0} cannot be found";        
    }
    
    public static class GeneralErrorMessages
    {
        public static readonly string UnexpectedErrorOccured = "Unexpected error occurred while processing your request";
    }
}