
public class Message : IMessage
{
    public string Type { get; set; }
    public Payload Payload { get; set; }
    public Meta Meta { get; set; }

    public Message()
    {
        Type = string.Empty;
        Payload = new Payload();
        Meta = new Meta();
    }
}

public class Payload
{
    public string TransactionId { get; set; }
    public string Message { get; set; }
    public string Title { get; set; }

    public Payload()
    {
        TransactionId = string.Empty;
        Message = string.Empty;
        Title = string.Empty;
    }
}

public class Meta
{
    public long Timestamp { get; set; }

    public Meta()
    {
        Timestamp = 0;
    }
}
