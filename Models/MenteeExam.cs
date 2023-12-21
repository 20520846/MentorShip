namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MenteeExam
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("mentorId")]
    public string MentorId { get; set; }

    [JsonPropertyName("menteeId")]
    public string MenteeId { get; set; }

    [JsonPropertyName("examId")]
    public string ExamId { get; set; }

    [JsonPropertyName("numberAns")]
    public int NumberAns { get; set; } = 0;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class Answer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("menteeExamId")]
    public string MenteeExamId { get; set; }

    [JsonPropertyName("questionId")]
    public string QuestionId { get; set; }

    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }

    [JsonPropertyName("answeredOption")]
    public int AnsweredOption { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
