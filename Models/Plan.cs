namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum NamePlan
{
    // None,
    Lite,
    Standard,
    Pro
}
public class Plan
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; } = null;

    [JsonPropertyName("name")]
    public NamePlan Name { get; set; } = NamePlan.Lite;

    [JsonPropertyName("callTimes")]
    public int CallTimes { get; set; } = 0;

    [JsonPropertyName("weeks")]
    public int Weeks { get; set; } = 0;

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;
}