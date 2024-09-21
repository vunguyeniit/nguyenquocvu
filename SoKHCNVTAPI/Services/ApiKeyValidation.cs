using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkiaSharp;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Services
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey, string ip);
    }

    public class ApiKeyValidation: IApiKeyValidation
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCachingService _iMemoryCachingService;

        public ApiKeyValidation(IConfiguration configuration, IMemoryCachingService iMemoryCachingService)
        {
            _configuration = configuration;
            _iMemoryCachingService = iMemoryCachingService;
        }
        public bool IsValidApiKey(string userApiKey, string ip)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            //string? apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
            //if (apiKey == null || apiKey != userApiKey)
            //    return false;
            // Check DB or file
            List<APIKeyModel>? _apiKeyList = _iMemoryCachingService.Get<List<APIKeyModel>>("KHCN_ACCESS_KEY");

            if (_apiKeyList == null)
            {
                string finalSubDirectory = Path.Combine("Assets", "APIKeys.json");
                if (File.Exists(finalSubDirectory))
                {
                    string json = File.ReadAllText(finalSubDirectory);
                    if (json != null)
                    {
                        _apiKeyList = JsonConvert.DeserializeObject<List<APIKeyModel>>(json);
                        if (_apiKeyList != null)
                        {
                            _iMemoryCachingService.Set<List<APIKeyModel>>("KHCN_ACCESS_KEY", _apiKeyList, 60*7*24);
                        }
                    }
                }
            }

            if (_apiKeyList != null)
            {
                if (_apiKeyList.Count > 0)
                {
                    foreach (APIKeyModel model in _apiKeyList)
                    {
                        if (model.IP == "")
                        {
                            if (model.Key == userApiKey)
                            {
                                return true;
                            }
                        } else
                        {
                            if (model.IP.Contains(ip))
                            {
                                if (model.Key == userApiKey  || userApiKey == "::1")
                                {
                                    return true;
                                }
                            }
                        }
                       
                    }
                }
            }

            return false;
        }
    }
}
