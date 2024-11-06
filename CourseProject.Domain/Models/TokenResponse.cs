using System.Text.Json.Serialization;

namespace CourseProject.Domain.Models;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}