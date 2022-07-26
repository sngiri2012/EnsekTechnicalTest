using Newtonsoft.Json;
using System;

namespace EnsekTechnicalTest.DTO.Response
{
    class PreviousOrderDetailsDTO
    {

        [JsonProperty("fuel")]
        public string Fuel { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }
    }
}
