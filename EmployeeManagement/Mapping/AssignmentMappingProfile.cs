using AutoMapper;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.ApiResources;
using EmployeeManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;
using static Constants;

namespace EmployeeManagement.Mapping
{
    public class AssignmentMappingProfile : Profile
    {
        public AssignmentMappingProfile()
        {
            // Domain to ViewModels
            CreateMap<IEnumerable<Assignment>, AssignmentListViewModel>()
                .ForMember(vm => vm.Assignments, opt => opt.MapFrom(src => src))
                .ForMember(vm => vm.SearchTerm, opt =>
                    opt.MapFrom((src, vm, destMember, ctx) => 
                        vm.SearchTerm = ctx.Items[MapperItemsKeys.SearchTerm] as string));

            CreateMap<Assignment, AssignmentFormViewModel>()
                .ForMember(vm => vm.Employees, opt => opt.Ignore());

            CreateMap<IEnumerable<Employee>, AssignmentFormViewModel>()               
               .AfterMap((src, vm, ctx) => {
                   vm.Employees = src.Select(e => new EmployeeVeiwModel
                   {
                       EmployeeId = e.Id,
                       EmployeeName = e.Name,
                       DepartmentName = e.Department.Name
                   })
                   .ToList();

                   if (!ctx.Items.ContainsKey(MapperItemsKeys.SelectedEmployees)) return;

                   var selectedEmployees = ctx.Items[MapperItemsKeys.SelectedEmployees] as ILookup<int, EmployeeAssignment>;                 

                   foreach (var employee in vm.Employees)
                       employee.IsSelected = selectedEmployees.Contains(employee.EmployeeId);
               });

            // ViewModels to Domain
            CreateMap<AssignmentFormViewModel, Assignment>()
                .ForMember(a => a.Id, opt => opt.Ignore())
                .ForMember(a => a.Employees, opt => opt.Ignore())
                .AfterMap((vm, a) => {
                    // Selected emploeyees' ids
                    var ids = vm.Employees
                        .Where(a => a.IsSelected)
                        .Select(a => a.EmployeeId)
                        .ToList();

                    // Remove unselected employees
                    var removedEmployees = a.Employees
                        .Where(ea => !ids.Contains(ea.EmployeeId))
                        .ToList();

                    foreach (var employee in removedEmployees)
                        a.Employees.Remove(employee);

                    // Add new employees
                    var addedEmployees = ids.Where(id => !a.Employees.Any(ea => ea.EmployeeId == id))
                        .Select(id => new EmployeeAssignment { EmployeeId = id })
                        .ToList();

                    foreach (var employee in addedEmployees)
                        a.Employees.Add(employee);
                });

            // Domain to API Resource
            CreateMap<Assignment, AssignmentResource>();
        }
    }
}
