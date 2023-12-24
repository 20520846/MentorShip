public class Event
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [JsonPropertyName("start")]
    public DateTime? Start { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [JsonPropertyName("end")]
    public DateTime? End { get; set; }

    [JsonPropertyName("weeks")]
    public int? Weeks { get; set; }

    [JsonPropertyName("isBusy")]
    public bool? IsBusy { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }
}
