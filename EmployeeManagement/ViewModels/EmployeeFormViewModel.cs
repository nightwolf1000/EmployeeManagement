using EmployeeManagement.Controllers;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Birth date")]
        [Required]        
        [Remote("IsBirthDateValid", "Validation")]
        public string BirthDate { get; set; }

        [Display(Name = "Hire date")]
        [Required]        
        [Remote("IsHireDateValid", "Validation", AdditionalFields = nameof(BirthDate))]
        public string HireDate { get; set; }

        [Display(Name = "Department")]
        [Required]
        public int? DepartmentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Position { get; set; }

        public IEnumerable<Department> Departments { get; set; }        

        public string ViewHeading => Id == 0 ? ViewHeadings.AddAnEmployee : ViewHeadings.EditAnEmployee;

        public string Action =>
            Id == 0 ? nameof(EmployeesController.Create) : nameof(EmployeesController.Update);

        public IList<AssignmentViewModel> Assignments { get; set; }        

        public EmployeeFormViewModel() => Assignments =  new List<AssignmentViewModel>();        

        public DateTime GetBirthDate() => BirthDate.ToDateTime();       

        public DateTime GetEmployementDate() => HireDate.ToDateTime();           
    }
}
