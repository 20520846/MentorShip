namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum PaymentStatus
{
    Success,
    Failed
}

public class Payment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("menteeId")]
    public string MenteeId { get; set; } = null;

    [JsonPropertyName("courseId")]
    public string CourseId { get; set; } = null;

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("status")]
    public PaymentStatus Status { get; set; } = PaymentStatus.Failed;
}
