namespace  MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; } = null;

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("ratingStar")]
    public double RatingStar { get; set; } = 0;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
