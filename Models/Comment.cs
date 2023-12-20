namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("ratingStar")]
    public double RatingStar { get; set; } = 0;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // id of the user who commented
    [JsonPropertyName("menteeId")]
    public string MenteeId { get; set; } = string.Empty;

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; } = string.Empty;

}