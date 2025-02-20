using System.Text.Json.Serialization;

namespace My.ZhiCore.Cli.Dto;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}