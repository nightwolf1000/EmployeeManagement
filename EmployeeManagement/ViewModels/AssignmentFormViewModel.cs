using EmployeeManagement.Controllers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Constants;

namespace EmployeeManagement.ViewModels
{
    public class AssignmentFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }       

        public string ViewHeading => Id == 0 ? ViewHeadings.AddAnAssignment : ViewHeadings.EditAnAssignment;        

        public string Action => 
            Id == 0 ? nameof(EmployeesController.Create) : nameof(EmployeesController.Update);       

        public IList<EmployeeVeiwModel> Employees { get; set; }

        public AssignmentFormViewModel() => Employees = new List<EmployeeVeiwModel>();
    }
}
