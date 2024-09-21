using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using System.Net;
using System.Text.Json;

namespace SoKHCNVTAPI.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception appError)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (appError)
                {
                    case RepositoryException e:
                        // Custom repository/service error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // An unhandled error in the application
                        _logger.LogError(appError, appError.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { 
                    Message = appError?.Message ?? "Error",
                    ErrorCode = (int)APIErrorCode.Unknown
                });
                await response.WriteAsync(result);
            }
        }
    }
}
