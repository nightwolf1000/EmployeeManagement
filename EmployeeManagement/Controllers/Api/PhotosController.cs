using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.ApiResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Extensions;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Utitlity.Exceptions;
using EmployeeManagement.Utitlity;
using static Constants;

namespace EmployeeManagement.Controllers.Api
{
    [CustomApiAuthorize(RoleNames.Administrator)]
    [Route("/api/employees/{employeeId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {        
        private readonly IMapper _mapper;        
        private readonly IUnitOfWork _unitOfWork;

        public PhotosController(IMapper mapper, IUnitOfWork unitOfWork)
        {           
            _mapper = mapper;            
            _unitOfWork = unitOfWork;
        }
                
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int employeeId, [FromForm]IFormFile file)
        {
            var employee = await _unitOfWork.Employees.GetEmployee(employeeId, e => e.Photo);

            if (employee == null) throw new ItemNotFoundCustomException(employeeId, NotFoundMessages.EmployeeCannotBeFound);            
            if (file == null) throw new BadRequestCustomException(UploadErrorMessages.FileCannotBeNull);            
            if (file.Length == 0) throw new BadRequestCustomException(UploadErrorMessages.FileCannotBeEmpty);            
            if (file.Length > UploadsSettings.MaxFileSize) throw new BadRequestCustomException(UploadErrorMessages.MaxFileSizeExceeded);            
            if (!file.IsSupported()) throw new BadRequestCustomException(UploadErrorMessages.InvalidFileType);            

            employee.SetPhoto(new Photo { ImageData = file.GetImageData() });            

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Photo, PhotoResource>(employee.Photo));
        }       
    }
}
