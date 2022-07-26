using Newtonsoft.Json;

namespace EnsekTechnicalTest.DTO.Response
{
    public class BuyOrderResponseDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
