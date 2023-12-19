namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum ApprovalStatus
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

    [JsonPropertyName("status")]
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
            
    [JsonPropertyName("applicationDate")]
    public DateTime ApplicationDate { get; set; } = DateTime.Now;
        
    [JsonPropertyName("mentorProfile")]
    public Mentor MentorProfile { get; set; } = new Mentor();
        
    [JsonPropertyName("reasonToBeMentor")]
    public string ReasonToBeMentor { get; set; } = string.Empty;
        
    [JsonPropertyName("greatestAchievement")]
    public string GreatestAchievement { get; set; } = string.Empty;
}
