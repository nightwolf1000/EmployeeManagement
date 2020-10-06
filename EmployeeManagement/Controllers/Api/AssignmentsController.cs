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
    [Route("/api/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssignmentsController(IMapper mapper, IUnitOfWork unitOfWork)
        {            
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }        
        
        [AllowAnonymous]
        [Route("/api/employees/{employeeId}/assignments")]        
        public async Task<IEnumerable<AssignmentResource>> GetAssignmentsForEmployee(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetEmployee(employeeId);

            if (employee == null)
                throw new ItemNotFoundCustomException(employeeId, NotFoundMessages.EmployeeCannotBeFound);

            var assignments = await _unitOfWork.Assignments.GetAssignmentsByEmployeeId(employeeId);
            
            return _mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentResource>>(assignments);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _unitOfWork.Assignments.GetAssignment(id);

            if (assignment == null)
                throw new ItemNotFoundCustomException(id, NotFoundMessages.AssignmentCannotBeFound);

            _unitOfWork.Assignments.Remove(assignment);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
