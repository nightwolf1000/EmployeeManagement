using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManagement.Core.Domain
{
    public class Employee
    {       
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }        
        public int DepartmentId { get; set; }       
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public Photo Photo { get; private set; }
        public ICollection<EmployeeAssignment> Assignments { get; set; }
        public Employee() => Assignments = new Collection<EmployeeAssignment>();
        
        public void SetPhoto(Photo photo)
        {            
            if (Photo == null)
                Photo = photo;
            else
                Photo.ImageData = photo.ImageData;
        }

        public int GetAssignmentNumber() => Assignments.Count;
    }
}
