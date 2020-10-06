using AutoMapper;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.ApiResources;

namespace EmployeeManagement.Mapping
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            // Domain to API resources
            CreateMap<Department, DepartmentResource>();
        }
    }
}
