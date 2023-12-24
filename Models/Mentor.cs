namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;



public class Mentor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("applicationId")]
    public string? ApplicationId { get; set; }

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; } = string.Empty;

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; } = null;

    [JsonPropertyName("bio")]
    public string Bio { get; set; } = string.Empty;

    [JsonPropertyName("linkedin")]
    public string LinkedIn { get; set; } = string.Empty;

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("skillIds")]
    public List<string> SkillIds { get; set; } = new List<string>();
                
    [JsonPropertyName("imageUrls")]
    public List<string> ImageUrls { get; set; } = new List<string>();
                        
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("ratingStar")]
    public double? RatingStar { get; set; } = 0;

    [JsonPropertyName("introduction")]
    public string? Introduction { get; set; } = string.Empty;

}