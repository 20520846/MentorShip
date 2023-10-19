using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;


public class MyHandler : IHandleMessages<NotificationMessage>
{
    static ILog log = LogManager.GetLogger<MyHandler>();

    public Task Handle(NotificationMessage message, IMessageHandlerContext context)
    {
        log.Info("Message received at endpoint ");
        return Task.CompletedTask;
    }
}
