using EmployeeManagement.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
    }
}
