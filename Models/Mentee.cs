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

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; } = string.Empty;

    [JsonPropertyName("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; } = null;
  
}
