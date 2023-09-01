namespace OrderManagement.Application.Common.Models;

public record Message
{
    [JsonPropertyName("type")]
    public MessageType Type { get; set; }
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}