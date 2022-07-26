using Newtonsoft.Json;

namespace EnsekTechnicalTest.DTO
{
    public class AccessTokenDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
