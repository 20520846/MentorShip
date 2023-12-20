namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


public class Mentee
{
    [
        BsonId,
        BsonRepresentation(BsonType.ObjectId),
        JsonPropertyName("id")
    ]
    public string? Id { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string? Email { get; set; } = string.Empty;

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; } = null;

}
