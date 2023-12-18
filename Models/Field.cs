
namespace MentorShip.Models;
using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Field
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; set; } = null;
}
