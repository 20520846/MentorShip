namespace MentorShip.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; } = null;


    [JsonPropertyName("name")]
    public string Name { get; set; } = null;

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("ratingStar")]
    public double RatingStar { get; set; } = 0;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("skillIds")]
    public List<string> SkillIds { get; set; } = new List<string>();

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; set; } = null;
}
