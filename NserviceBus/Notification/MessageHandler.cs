using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;


public class MessageHandler : IHandleMessages<Message>
{
    static ILog log = LogManager.GetLogger<MessageHandler>();

    public Task Handle(Message message, IMessageHandlerContext context)
    {
        log.Info("Message received at endpoint ");
        return Task.CompletedTask;
    }
}
