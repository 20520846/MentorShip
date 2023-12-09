using MentorShip.Models;
using MentorShip.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private IMessageSession _messageSession;

        public PaymentController(PaymentService paymentService, IMessageSession messageSession)
        {
            _paymentService = paymentService;
            _messageSession = messageSession;

        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                var createdPayment = await _paymentService.CreatePayment(payment);
                var message = new Message
                {
                    Type = "PAYMENT",
                    Payload = new Payload
                    {
                        TransactionId = payment.Id,
                        Title = "Giao dịch thành công",
                        Message = "",
                    },
                    Meta = new Meta
                    {
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    }
                };

                await _messageSession.Send(message).ConfigureAwait(false);
                return Ok(new { data = createdPayment });
            }
            catch (ArgumentException ex)
            {
                // Handle client error
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle server error
                var message = new Message
                {
                    Type = "PAYMENT",
                    Payload = new Payload
                    {
                        TransactionId = payment.Id,
                        Title = "Lỗi thanh toán",
                        Message = ex.Message,
                    },
                    Meta = new Meta
                    {
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    }
                };

                await _messageSession.Send(message).ConfigureAwait(false);
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            return Ok(new { data = payments });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentsByUserId(string userId)
        {
            var payments = await _paymentService.GetPaymentsByUserId(userId);
            return Ok(new { data = payments });
        }
    }
}
