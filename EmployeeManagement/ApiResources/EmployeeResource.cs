namespace EmployeeManagement.ApiResources
{
    public class EmployeeResource
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public DepartmentResource Department { get; set; }        
    }
}
