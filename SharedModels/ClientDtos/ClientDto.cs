using System.Text.Json.Serialization;

namespace SharedModels.ClientDtos;

public class ClientDto
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("system")]
  public required string System { get; set; }

  [JsonPropertyName("address")]
  public required string Address { get; set; }

  [JsonPropertyName("phone")]
  public required string Phone { get; set; }

  [JsonPropertyName("notes")]
  public string? Notes { get; set; }

  [JsonPropertyName("applicationUserId")]
  public required string ApplicationUserId { get; set; }
}
