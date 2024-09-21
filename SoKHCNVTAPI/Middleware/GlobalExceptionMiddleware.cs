using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var instance = new ExceptionBaseResponse
        {
            Success = false,
            Message = ex.Message
        };

        switch (ex)
        {
            case ArgumentException:
            case SecurityTokenException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                instance.ErrorCode = (int)APIErrorCode.Params_Invalid;
                break;
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                instance.ErrorCode = (int)APIErrorCode.Unknown;
                break;
        }

        return context.Response.WriteAsync(instance.ToString());
    }
}