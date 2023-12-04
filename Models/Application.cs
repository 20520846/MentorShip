namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum ApplicationStatus
{
    Approved,
    Rejected, 
    Accepted   
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
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("status")]
    public ApplicationStatus Status { get; set; }

    [JsonPropertyName("imageUrls")]
    public List<string> ImageUrls { get; set; } = new List<string>();

    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;
}
