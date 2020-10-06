using AutoMapper;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeManagement.Utitlity.Exceptions;
using static Constants;
using System.Linq;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class AssignmentsController : Controller
    {        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;        

        public AssignmentsController(IMapper mapper, IUnitOfWork unitOfWork)
        {            
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
        }

        [AllowAnonymous]
        public async Task<IActionResult> List(string searchTerm = null)
        {            
            var assignments = await _unitOfWork.Assignments.GetAssignments(searchTerm, a => a.Employees);

            var viewModel = _mapper.Map<AssignmentListViewModel>(assignments, opt => 
                opt.Items[MapperItemsKeys.SearchTerm] = searchTerm);

            if (User.Identity.IsAuthenticated && User.IsInRole(RoleNames.Administrator))
                return View("List", viewModel);

            return View("ReadOnlyList", viewModel);
        }

        [AllowAnonymous]
        [HttpPost]        
        public IActionResult Search(AssignmentListViewModel viewModel) => 
            RedirectToAction(nameof(List), new { searchTerm = viewModel.SearchTerm });

        public async Task<IActionResult> Create()
        {
            var employees = await _unitOfWork.Employees.GetEmployees(null, e => e.Department);
            return View("AssignmentForm", _mapper.Map<AssignmentFormViewModel>(employees, opt => { }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("AssignmentForm", viewModel);            
            
            await _unitOfWork.Assignments.AddAssignment(_mapper.Map<Assignment>(viewModel));
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var assignment = await _unitOfWork.Assignments.GetAssignment(id, a => a.Employees);

            if (assignment == null)
                throw new ItemNotFoundCustomException(id, NotFoundMessages.AssignmentCannotBeFound);

            var employees = await _unitOfWork.Employees.GetEmployees(null, e => e.Department);                         

            var viewModel = _mapper.Map<AssignmentFormViewModel>(assignment);
            _mapper.Map(employees, viewModel, opt =>
                opt.Items[MapperItemsKeys.SelectedEmployees] = assignment.Employees.ToLookup(ea => ea.EmployeeId));
            
            return View("AssignmentForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AssignmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("AssignmentForm", viewModel);

            var assignment = await _unitOfWork.Assignments.GetAssignment(viewModel.Id, a => a.Employees);

            if (assignment == null)
                throw new ItemNotFoundCustomException(viewModel.Id, NotFoundMessages.AssignmentCannotBeFound);

            _mapper.Map(viewModel, assignment);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(List));            
        }
    }
}
