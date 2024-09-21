using Microsoft.AspNetCore.Mvc;

namespace SoKHCNVTAPI.Services
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
