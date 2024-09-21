using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class ExceptionBaseResponse : BaseResponse
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
    }
}