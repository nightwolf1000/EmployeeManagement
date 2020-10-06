namespace EmployeeManagement.Core.Domain
{
    public class EmployeeAssignment
    {        
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }        
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
