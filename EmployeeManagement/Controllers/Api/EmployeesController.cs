using AutoMapper;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.ApiResources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Utitlity.Exceptions;
using EmployeeManagement.Utitlity;
using static Constants;

namespace EmployeeManagement.Controllers.Api
{
    [CustomApiAuthorize(RoleNames.Administrator)]
    [Route("/api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {        
        private readonly IMapper _mapper;             
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IMapper mapper, IUnitOfWork unitOfWork)
        {            
            _mapper = mapper;                    
            _unitOfWork = unitOfWork;
        }        
        
        [AllowAnonymous]
        [Route("/api/assignments/{assignmentId}/employees")]
        public async Task<IEnumerable<EmployeeResource>> GetEmployeesOnAssignment(int assignmentId)
        {
            var assignment = await _unitOfWork.Assignments.GetAssignment(assignmentId);

            if (assignment == null)
                throw new ItemNotFoundCustomException(assignmentId, NotFoundMessages.AssignmentCannotBeFound);

            var employees = await _unitOfWork.Employees.GetEmployeesByAssignmentId(assignmentId);

            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.Employees.GetEmployee(id);

            if (employee == null)
                throw new ItemNotFoundCustomException(id, NotFoundMessages.EmployeeCannotBeFound);
            
            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
