using MentorShip.Models;
using MentorShip.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    public class PaymentInfor
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
    }

    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private IMessageSession _messageSession;
        private readonly IConfiguration _configuration;

        public PaymentController(PaymentService paymentService, IMessageSession messageSession, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _messageSession = messageSession;
            _configuration = configuration;

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

        [HttpGet("/getRequestUrl")]
        public async Task<IActionResult> GetRequesturl([FromBody] PaymentInfor payment)
        {
            try
            {
                var vnpayConfig = new
                {
                    TmnCode = _configuration["VNPAY:TmnCode"],
                    HashSecret = _configuration["VNPAY:HashSecret"],
                    BaseUrl = _configuration["VNPAY:BaseUrl"],
                    Command = _configuration["VNPAY:Command"],
                    CurrCode = _configuration["VNPAY:CurrCode"],
                    Version = _configuration["VNPAY:Version"],
                    Locale = _configuration["VNPAY:Locale"],
                };

                var vnpay = new VnPayLibrary();
                var Utils = new Utils();

                vnpay.AddRequestData("vnp_Version", vnpayConfig.Version);
                vnpay.AddRequestData("vnp_Command", vnpayConfig.Command);
                vnpay.AddRequestData("vnp_TmnCode", vnpayConfig.TmnCode);
                vnpay.AddRequestData("vnp_Amount", (payment.Amount * 100).ToString());
                vnpay.AddRequestData("vnp_OrderInfo", payment.Description);
                vnpay.AddRequestData("vnp_OrderType", "billpayment");
                vnpay.AddRequestData("vnp_ReturnUrl", "https://localhost:5008/swagger/index.html");
                vnpay.AddRequestData("vnp_CurrCode", vnpayConfig.CurrCode);
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(
                    HttpContext
                )); // Ensure Utils.GetIpAddress() is implemented
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.AddHours(7).ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString());
                vnpay.AddRequestData("vnp_Locale", vnpayConfig.Locale);

                // Add other parameters as needed

                // Create URL
                var paymentUrl = vnpay.CreateRequestUrl(vnpayConfig.BaseUrl, vnpayConfig.HashSecret);

                Console.WriteLine("VNPAY API URL:");
                Console.WriteLine(paymentUrl);

                return Ok(new { data = paymentUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
