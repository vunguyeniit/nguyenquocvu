using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Enums;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Net;

namespace SoKHCNVTAPI.Services
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;
        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string ip = "";
            IPAddress? iPAddress = context.HttpContext.Connection.RemoteIpAddress;
            if(iPAddress != null)
            {
                ip = iPAddress.ToString();
            }
            string userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName].ToString();
            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }
            if (!_apiKeyValidation.IsValidApiKey(userApiKey, ip))
            {
                context.Result = new UnauthorizedResult();
            }
                
        }
    }
}
