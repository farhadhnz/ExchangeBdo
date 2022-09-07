using System.Text.Json.Serialization;

namespace ExchangeBdo.SharedDtos
{
    public class ExchangeDto
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}