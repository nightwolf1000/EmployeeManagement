namespace EmployeeManagement.Core.Domain
{
    public class Photo
    {
        public int Id { get; set; }        
        public byte[] ImageData { get; set; }
        public Employee Employee;
        public int EmployeeId { get; set; }
    }
}
