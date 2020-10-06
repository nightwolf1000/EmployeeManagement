using AutoMapper;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Utitlity.Exceptions;
using static Constants;
using System.Linq;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class EmployeesController : Controller
    {        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public EmployeesController(IMapper mapper, IUnitOfWork unitOfWork)
        {           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public async Task<IActionResult> List(string searchTerm = null)
        {
            var employees = await _unitOfWork.Employees.GetEmployees(searchTerm, e => e.Assignments, e => e.Department);

            var viewModel = _mapper.Map<EmployeeListViewModel>(employees, opt => 
                opt.Items[MapperItemsKeys.SearchTerm] = searchTerm);

            if (User.Identity.IsAuthenticated && User.IsInRole(RoleNames.Administrator))
                return View("List", viewModel);

            return View("ReadOnlyList", viewModel);
        }

        [AllowAnonymous]
        [HttpPost]        
        public IActionResult Search(EmployeeListViewModel viewModel) => 
            RedirectToAction(nameof(List), new { searchTerm = viewModel.SearchTerm });

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _unitOfWork.Employees.GetEmployee(id, e => e.Assignments, e => e.Department, e => e.Photo);

            if (employee == null)
                throw new ItemNotFoundCustomException(id, NotFoundMessages.EmployeeCannotBeFound);

            return View(_mapper.Map<Employee, EmployeeDetailsViewModel>(employee));
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.Departments.GetDepartments();
            var assignments = await _unitOfWork.Assignments.GetAssignments();

            var viewModel = _mapper.Map<EmployeeFormViewModel>(departments);
            _mapper.Map(assignments, viewModel, opt => { });

            return View("EmployeeForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _unitOfWork.Departments.GetDepartments();
                _mapper.Map(departments, viewModel);
                return View("EmployeeForm", viewModel);
            }             

            await _unitOfWork.Employees.AddEmployee(_mapper.Map<Employee>(viewModel));
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _unitOfWork.Employees.GetEmployee(id, e => e.Assignments);

            if (employee == null)
                throw new ItemNotFoundCustomException(id, NotFoundMessages.EmployeeCannotBeFound);

            var departments = await _unitOfWork.Departments.GetDepartments();
            var assignments = await _unitOfWork.Assignments.GetAssignments();            

            var viewModel = _mapper.Map<EmployeeFormViewModel>(employee);
            _mapper.Map(departments, viewModel);
            _mapper.Map(assignments, viewModel, opt =>
                opt.Items[MapperItemsKeys.SelectedAssignments] = employee.Assignments.ToLookup(ea => ea.AssignmentId));

            return View("EmployeeForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _unitOfWork.Departments.GetDepartments();
                _mapper.Map(departments, viewModel);
                return View("EmployeeForm", viewModel);
            }

            var employee = await _unitOfWork.Employees.GetEmployee(viewModel.Id, e => e.Assignments);

            if (employee == null)
                throw new ItemNotFoundCustomException(viewModel.Id, NotFoundMessages.EmployeeCannotBeFound);

            _mapper.Map(viewModel, employee);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(List));
        }
    }
}
