using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SoKHCNVTAPI.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class MultipartFormDataAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        if (request is { ContentType: not null, HasFormContentType: true }
            && request.ContentType.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        context.Result = new StatusCodeResult(StatusCodes.Status415UnsupportedMediaType);
    }
}