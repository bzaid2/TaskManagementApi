using Carter.ModelBinding;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Shared;

namespace TaskManagement.Infrastructure.Middleware
{
    internal class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = new ProblemDetails
                {
                    Status = httpContext.Response.StatusCode,
                    Title = ((HttpStatusCode)httpContext.Response.StatusCode).ToString(),
                    Type = RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    Detail = "One or more validation errors occurred.",
                    Instance = httpContext.Request.Path
                };
                response.Extensions.Add("errors", new ValidationResult
                {
                    Errors = ex.Errors.ToList(),
                }.GetValidationProblems());
                await httpContext.Response.WriteAsJsonAsync(response, options: null, contentType: ProjectConstants.CONTENT_TYPE_APPLICATION_ERROR);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = httpContext.Response.StatusCode,
                    Title = ((HttpStatusCode)httpContext.Response.StatusCode).ToString(),
                    Type = RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    Instance = httpContext.Request.Path,
                    Detail = ex.Message,
                }, options: null, contentType: ProjectConstants.CONTENT_TYPE_APPLICATION_ERROR);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = new ProblemDetails
                {
                    Status = httpContext.Response.StatusCode,
                    Title = ((HttpStatusCode)httpContext.Response.StatusCode).ToString(),
                    Type = RfcDocumentation.GetDocumentation(HttpStatusCode.BadRequest),
                    Detail = "User and/or password are incorrect",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(response, options: null, contentType: ProjectConstants.CONTENT_TYPE_APPLICATION_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = httpContext.Response.StatusCode,
                    Title = ((HttpStatusCode)httpContext.Response.StatusCode).ToString(),
                    Type = RfcDocumentation.GetDocumentation(HttpStatusCode.InternalServerError),
                    Detail = "Something bad happened. Please come back later when we fixed that problem. Thanks",
                    Instance = httpContext.Request.Path
                }, options: null, contentType: ProjectConstants.CONTENT_TYPE_APPLICATION_ERROR);
            }
        }
    }

    internal static class ExceptionMiddlewareExtensions
    {
        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
