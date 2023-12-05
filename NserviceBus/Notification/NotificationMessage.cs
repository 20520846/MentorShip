public class NotificationMessage : ICommand
{
    public string Type { get; set; }
    public Payload Payload { get; set; }
    public Meta Meta { get; set; }

    public NotificationMessage()
    {
        Type = string.Empty;
        Payload = new Payload();
        Meta = new Meta();
    }
}

public class Payload
{
    public string UserId { get; set; }
    public string Message { get; set; }

    public Payload()
    {
        UserId = string.Empty;
        Message = string.Empty;
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
