using AutoMapper;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.ApiResources;

namespace EmployeeManagement.Mapping
{
    public class PhotoMappingProfile : Profile
    {
        public PhotoMappingProfile()
        {
            // Domain to API resources
            CreateMap<Photo, PhotoResource>();
        }
    }
}
