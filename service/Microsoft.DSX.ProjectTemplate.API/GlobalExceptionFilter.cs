using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;

namespace Microsoft.DSX.ProjectTemplate.API
{
    /// <summary>
    /// This filter will capture unhandled exceptions that occur in
    /// controller creation, model binding, action filters, or action methods.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#exception-filters</remarks>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionFilter"/> class.
        /// </summary>
        public GlobalExceptionFilter(IHostEnvironment hostingEnvironment, ILogger<GlobalExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            // transform the exception into JSON
            if (context.Exception is ExceptionBase)
            {
                _logger.LogInformation(context.Exception, context.ActionDescriptor.DisplayName);

                ExceptionBase customException = context.Exception as ExceptionBase;

                var exceptionJson = new ErrorResponseDto
                {
                    Message = customException.Message,
                    InnerExceptionMessage = customException.InnerException?.Message,
                };

                switch (_hostingEnvironment.EnvironmentName.ToLowerInvariant())
                {
                    case "local":
                    case "dev":
                    case "development":
                        exceptionJson.StackTrace = customException.StackTrace;
                        break;
                    default:
                        break;
                }

                context.Result = new JsonResult(exceptionJson) { StatusCode = (int)customException.StatusCode };
                context.ExceptionHandled = true;
            }
            else
            {
                // unexpected exception so log to _logger (which probably will be Application Insights)
                _logger.LogCritical(context.Exception, context.ActionDescriptor.DisplayName);

                // unhandled exception keeps percolating and will be handled by ASP.NET Core
            }
        }
    }
}
