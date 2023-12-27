namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics.Contracts;

public class MenteeApplicationModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; }

    [JsonPropertyName("plan")]
    public Plan Plan { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
        
    // thanh toán hay chưa
    [JsonPropertyName("paymentStatus")]
    public PaymentStatus PayStatus { get; set; } = PaymentStatus.Failed;

    //ngày bắt đầu học
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("applicationDate")]
    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    [JsonPropertyName("approvedDate")]
    public DateTime ApprovedDate { get; set; }

    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }

    // Additional fields
    [JsonPropertyName("mentorResponse")]
    public string? MentorResponse { get; set; } = string.Empty;

    [JsonPropertyName("menteeProfile")]
    public Mentee MenteeProfile { get; set; } = new Mentee();

    [JsonPropertyName("goal")]
    public string? Goal { get; set; } = string.Empty;

    [JsonPropertyName("personalDescription")]
    public string? PersonalDescription { get; set; } = string.Empty;

    [JsonPropertyName("expectation")]
    public string? Expectation { get; set; } = string.Empty;

    [JsonPropertyName("contactTimes")]
    public string? ContactTimes { get; set; } = string.Empty;

    [JsonPropertyName("learningTime")]
    public List<Event> LearningTime { get; set; } = new List<Event>();

    [JsonPropertyName("isPaid")]
    public bool? IsPaid { get; set; } = false;
    
}
