using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Calindra.Desafio.Domain.Helpers
{
    public static class Deserializers
    {
        public static T DeserializeJsonAsSnakeCaseNaming<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy { ProcessDictionaryKeys = true }
                }
            });
        }
    }
}
