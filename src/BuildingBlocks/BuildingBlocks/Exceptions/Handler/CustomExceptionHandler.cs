using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exception, DateTime.UtcNow);
            (string Detail, string Title, int StatusCode) details = exception switch
            {

                FluentValidation.ValidationException => (
                exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
                BadRequestException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
                
                OrderNotFoundException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status404NotFound),
                NotFoundException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status404NotFound),
                _ => throw new NotImplementedException()

            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("tranceId", context.TraceIdentifier);

            if (exception is FluentValidation.ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
