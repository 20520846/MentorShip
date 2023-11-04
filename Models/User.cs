namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("password")]
    public string? Password { get; set; } = null;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = null;
}
