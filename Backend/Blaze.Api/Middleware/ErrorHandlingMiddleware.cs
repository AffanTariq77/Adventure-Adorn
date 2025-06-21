using System.Net;

namespace Blaze.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            LogExceptionDetails(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorCode = Guid.NewGuid().ToString();

            var response = new
            {
                message = exception.Message,
                errorCode = errorCode,
                exceptionType = exception.GetType().Name,
                details = exception.InnerException?.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }

        private void LogExceptionDetails(Exception exception)
        {
            var currentException = exception;
            while (currentException != null)
            {
                _logger.LogError($"Exception: {currentException.Message}");
                _logger.LogError($"Stack Trace: {currentException.StackTrace}");

                currentException = currentException.InnerException;
            }
        }
    }
}
