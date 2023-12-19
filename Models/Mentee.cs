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

    [JsonPropertyName("price")]
    public decimal? Price { get; set; } = 0;

    [JsonPropertyName("ratingStar")]
    public double? RatingStar { get; set; } = 0;

    [JsonPropertyName("introduction")]
    public string? Introduction { get; set; } = string.Empty;

}
