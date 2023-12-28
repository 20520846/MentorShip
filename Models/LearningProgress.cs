
namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class LearningProgress
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	[JsonPropertyName("applicationId")]
	public string ApplicationId { get; set; }

	[JsonPropertyName("mentorId")]
	public string MentorId { get; set; } = string.Empty;

	[JsonPropertyName("menteeId")]
	public string MenteeId { get; set; } = string.Empty;

	[JsonPropertyName("startDate")]
	public DateTime StartDate { get; set; }

	[JsonPropertyName("endDate")]
	public DateTime EndDate { get; set; }

	[JsonPropertyName("callTimesLeft")]
	public int CallTimesLeft { get; set; }

	[JsonPropertyName("additionalCallTimes")]
	public int AdditionalCallTimes { get; set; }
}

public class LearningTestProgress
{
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	[JsonPropertyName("applicationId")]
	public string ApplicationId { get; set; }

	[JsonPropertyName("mentorId")]
	public string MentorId { get; set; } = string.Empty;

	[JsonPropertyName("menteeId")]
	public string MenteeId { get; set; } = string.Empty;

	[JsonPropertyName("startDate")]
	public DateTime StartDate { get; set; } = DateTime.Now;

	[JsonPropertyName("endDate")]
	public DateTime EndDate { get; set; } = DateTime.Now + TimeSpan.FromDays(7);

	[JsonPropertyName("callTimesLeft")]
	public int CallTimesLeft { get; set; } = 1;
}


