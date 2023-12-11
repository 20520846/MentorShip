namespace MentorShip.Models;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Mentee
{
    [
        BsonId,
        BsonRepresentation(BsonType.ObjectId),
        JsonPropertyName("id")
    ]
    public string? Id { get; set; }

    [
     BsonId,
     BsonRepresentation(BsonType.ObjectId),
     JsonPropertyName("userId")
 ]
    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    

}