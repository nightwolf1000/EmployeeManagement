using EmployeeManagement.ApiResources;
using EmployeeManagement.Utitlity.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using static Constants;

namespace EmployeeManagement.Controllers.Api
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(ILogger<ErrorsController> logger) => _logger = logger;

        [Route("/api/errors")]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (feature.Error is CustomErrorResponseException customException)
            {
                _logger.LogWarning("The path {path} threw an exception {exception}", feature.Path, customException);
                Response.StatusCode = customException.StatusCode;

                return new ObjectResult(new CustomErrorResource {
                    ErrorDefinition = customException.ErrorDefinition,
                    Message = customException.Message
                });
            }

            _logger.LogError("The path {path} threw an exception {exception}", feature.Path, feature.Error);
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;           

            return new ObjectResult(new CustomErrorResource {
                ErrorDefinition = ErrorDefinitions.InternalServerError,
                Message = GeneralErrorMessages.UnexpectedErrorOccured
            });
        }

        [Route("/api/errors/404")]
        public IActionResult HandleNotFoundStatusCode()
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            _logger.LogWarning("404 error occured. Path = {originalPath} and QueryString = {queryString}",
                feature.OriginalPath, feature.OriginalQueryString);

            return new ObjectResult(new CustomErrorResource {
                ErrorDefinition = ErrorDefinitions.NotFoundError,
                Message = NotFoundMessages.ResourceCannotBeFound
            });
        }
    }
}
