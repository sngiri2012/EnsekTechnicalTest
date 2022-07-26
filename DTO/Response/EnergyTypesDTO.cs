using Newtonsoft.Json;

namespace EnsekTechnicalTest.DTO.Response
{

    public partial class EnergyTypesDTO
    {
        [JsonProperty("electric")]
        public EnergyDetails Electric { get; set; }

        [JsonProperty("gas")]
        public EnergyDetails Gas { get; set; }

        [JsonProperty("nuclear")]
        public EnergyDetails Nuclear { get; set; }

        [JsonProperty("oil")]
        public EnergyDetails Oil { get; set; }
    }

    public partial class EnergyDetails
    {

        [JsonProperty("energy_id")]
        public long EnergyId { get; set; }

        [JsonProperty("price_per_unit")]
        public double PricePerUnit { get; set; }

        [JsonProperty("quantity_of_units")]
        public long QuantityOfUnits { get; set; }

        [JsonProperty("unit_type")]
        public string UnitType { get; set; }
    }

}
