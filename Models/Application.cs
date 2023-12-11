namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum ApplicationStatus
{
    Pending,
    Approved,
    Rejected
}

public class Application
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("mentorId")]
    public string? MentorId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    [JsonPropertyName("imageUrls")]
    public List<string> ImageUrls { get; set; } = new List<string>();

    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;
}
