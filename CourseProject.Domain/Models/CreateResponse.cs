using System.Text.Json.Serialization;

namespace CourseProject.Domain.Models;

public class CreateResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}