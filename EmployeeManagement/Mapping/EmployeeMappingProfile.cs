using AutoMapper;
using EmployeeManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.ApiResources;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Extensions;
using static Constants;

namespace EmployeeManagement.Mapping
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            // Domain to ViewModels
            CreateMap<IEnumerable<Employee>, EmployeeListViewModel>()
                .ForMember(vm => vm.Employees, opt => opt.MapFrom(src => src))
                .ForMember(vm => vm.SearchTerm, opt => 
                    opt.MapFrom((src, vm, destMember, ctx) => 
                        vm.SearchTerm = ctx.Items[MapperItemsKeys.SearchTerm] as string));

            CreateMap<Employee, EmployeeDetailsViewModel>()
                .ForMember(vm => vm.Employee, opt => opt.MapFrom(src => src));

            CreateMap<Employee, EmployeeFormViewModel>()
                .ForMember(vm => vm.Assignments, opt => opt.Ignore())
                .ForMember(vm => vm.BirthDate, opt => opt.MapFrom(e => e.BirthDate.ToCustomString()))
                .ForMember(vm => vm.HireDate, opt => opt.MapFrom(e => e.HireDate.ToCustomString()));       

            CreateMap<IEnumerable<Assignment>, EmployeeFormViewModel>()                
                .AfterMap((src, vm, ctx) => {
                    vm.Assignments = src.Select(a => new AssignmentViewModel 
                    {
                        AssignmentId = a.Id,
                        AssignmentName = a.Name
                    })
                    .ToList();

                    if (!ctx.Items.ContainsKey(MapperItemsKeys.SelectedAssignments)) return;

                    var selectedAssignments = ctx.Items[MapperItemsKeys.SelectedAssignments] as ILookup<int, EmployeeAssignment>;

                    foreach (var assignment in vm.Assignments)
                        assignment.IsSelected = selectedAssignments.Contains(assignment.AssignmentId);
                });

            CreateMap<IEnumerable<Department>, EmployeeFormViewModel>()
                .ForMember(vm => vm.Departments, opt => opt.MapFrom(src => src));            

            // ViewModels to Domain
            CreateMap<EmployeeFormViewModel, Employee>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.Assignments, opt => opt.Ignore())
                .ForMember(e => e.BirthDate, opt => opt.MapFrom(vm => vm.GetBirthDate()))
                .ForMember(e => e.HireDate, opt => opt.MapFrom(vm => vm.GetEmployementDate()))                
                .AfterMap((vm, e) => {
                    // Selected assignments' ids
                    var ids = vm.Assignments
                        .Where(a => a.IsSelected)
                        .Select(a => a.AssignmentId)
                        .ToList();

                    // Remove unselected assignments
                    var removedAssignments = e.Assignments
                        .Where(ea => !ids.Contains(ea.AssignmentId))
                        .ToList();

                    foreach (var assignment in removedAssignments)
                        e.Assignments.Remove(assignment);

                    // Add new assignments
                    var addedAssignment = ids.Where(id => !e.Assignments.Any(ea => ea.AssignmentId == id))
                        .Select(id => new EmployeeAssignment { AssignmentId = id })
                        .ToList();

                    foreach (var assignment in addedAssignment)
                        e.Assignments.Add(assignment);
                });

            // Domain to API resources
            CreateMap<Employee, EmployeeResource>();
        }       
    }
}
