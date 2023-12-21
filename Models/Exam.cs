using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MentorShip.Models
{
    public enum QuestionType
    {
        OneAnswer,
        MultipleAnswer,
        TrueFalse,
    }

    public class Exam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("mentorId")]
        public string MentorId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // [JsonPropertyName("questions")]
        // public List<Question> Questions { get; set; } = new List<Question>();
        [JsonPropertyName("numberQues")]
        public int NumberQues { get; set; } = 0;
    }

    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("examId")]
        public string ExamId { get; set; }

        [JsonPropertyName("type")]
        public QuestionType Type { get; set; } = QuestionType.OneAnswer;

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("options")]
        public List<QuestionOption> Options { get; set; }

        [JsonPropertyName("explain")]
        public string Explain { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class QuestionOption
    {
        [JsonPropertyName("option")]
        public string Option { get; set; }

        [JsonPropertyName("isCorrect")]
        public bool IsCorrect { get; set; }
    }
}