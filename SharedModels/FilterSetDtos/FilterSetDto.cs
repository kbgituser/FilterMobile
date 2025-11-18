using System.Text.Json.Serialization;

namespace SharedModels.FilterSetDtos;

public class FilterSetDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("filterType")]
    public string? FilterType { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("period")]
    public int Period { get; set; }

    [JsonPropertyName("applicationUserId")]
    public string? ApplicationUserId { get; set; }
}
