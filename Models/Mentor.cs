namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public enum ApprovalStatus
{
    Pending,
    Approved,
    Rejected
}

public class Mentor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    // [JsonPropertyName("email")]
    // public string Email { get; set; } = null;

    // [JsonPropertyName("phoneNumber")]
    // public string PhoneNumber { get; set; } = null;

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; } = null;

    [JsonPropertyName("company")]
    public string Company { get; set; } = null;

    [JsonPropertyName("skills")]
    public string[] Skills { get; set; } = Array.Empty<string>();

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("bio")]
    public string Bio { get; set; } = string.Empty;

    [JsonPropertyName("linkedin")]
    public string LinkedIn { get; set; } = string.Empty;

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}