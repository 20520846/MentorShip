using System;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using System.Threading.Tasks;

namespace MentorShip.Controllers;

[Controller]
[Route("api/[controller]")]
public class MessageController : Controller
{

    IMessageSession messageSession;
    #region MessageSessionInjection
    public MessageController(IMessageSession messageSession)
    {
        this.messageSession = messageSession;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> SendMessage()
    {
        var message = new NotificationMessage
        {
            Type = "USER_NOTIFICATION",
            Payload = new Payload
            {
                UserId = "123",
                Message = "Bạn có một thông báo mới"
            },
            Meta = new Meta
            {
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            }
        };
        await messageSession.Send(message).ConfigureAwait(false);
        return Ok("Endpoint API của bạn đã được gọi!");
    }


}