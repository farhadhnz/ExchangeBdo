using System.Text.Json.Serialization;

namespace ExchangeBdo.SharedDtos
{
    public class SymbolDto
    {
        [JsonPropertyName("symbols")]
        public Dictionary<string, string> Symbols { get; set; }
    }
}
