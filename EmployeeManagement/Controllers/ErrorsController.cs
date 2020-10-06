using EmployeeManagement.Utitlity.Exceptions;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using static Constants;

namespace EmployeeManagement.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(ILogger<ErrorsController> logger) => _logger = logger;
        
        [Route("/errors")]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (feature.Error is CustomErrorResponseException customException)
            {                
                _logger.LogWarning("The path {path} threw an exception {exception}", feature.Path, customException);
                Response.StatusCode = customException.StatusCode;

                return View("Error", new CustomErrorViewModel {
                    ErrorDefinition = customException.ErrorDefinition,
                    Message = customException.Message
                });                              
            }
            
            _logger.LogError("The path {path} threw an exception {exception}", feature.Path, feature.Error);
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return View("Error", new CustomErrorViewModel {
                ErrorDefinition = ErrorDefinitions.InternalServerError,
                Message = GeneralErrorMessages.UnexpectedErrorOccured
            });         
        }

        [Route("/errors/404")]
        public IActionResult HandleNotFoundStatusCode()
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            _logger.LogWarning("404 error occured. Path = {originalPath} and QueryString = {queryString}", 
                feature.OriginalPath, feature.OriginalQueryString);

            return View("Error", new CustomErrorViewModel {
                ErrorDefinition = ErrorDefinitions.NotFoundError,
                Message = NotFoundMessages.ResourceCannotBeFound
            });   
        }
    }
}
