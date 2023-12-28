using MentorShip.Models;
using MentorShip.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    public class PaymentInfor
    {
        public string MenteeId { get; set; }
        public string MenteeApplicattion { get; set; }

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
        private readonly MenteeApplicationService _menteeApplicationService;

        public PaymentController(PaymentService paymentService, IMessageSession messageSession, IConfiguration configuration, MenteeApplicationService menteeApplicationService)
        {
            _paymentService = paymentService;
            _messageSession = messageSession;
            _configuration = configuration;
            _menteeApplicationService = menteeApplicationService;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                var paymentCheck = await _paymentService.GetPaymentByMenteeApplicationId(payment.MenteeApplicationId);
                if (paymentCheck != null)
                {
                    return BadRequest(new { error = "Payment already exists" });
                }
                else
                {
                    var createdPayment = await _paymentService.CreatePayment(payment);
                    await _menteeApplicationService.UpdatePayStatus(payment.MenteeApplicationId, payment.Status);

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
        [HttpGet("getPaymentsByMentorId/{mentorId}")]
        public async Task<IActionResult> GetPaymentsByMentorId(string mentorId, [FromQuery] int? year)
        {
            var payments = await _paymentService.GetPaymentsByMentorId(mentorId, year);
            return Ok(new { data = payments });
        }

        [HttpDelete("deletePayments/{menteeApplicationId}")]
        public async Task<ActionResult> DeletePaymentsByMenteeApplicationId(string menteeApplicationId)
        {
            try
            {
                // Lấy danh sách các payment có cùng menteeApplicationId
                var payments = await _paymentService.GetPaymentsByMenteeApplicationId(menteeApplicationId);

                // Kiểm tra nếu có ít nhất 2 payment
                if (payments.Count >= 2)
                {
                    // Sắp xếp danh sách theo một tiêu chí nào đó (ví dụ: theo ngày tạo)
                    payments = payments.OrderBy(p => p.CreatedAt).ToList();

                    // Chọn payment cần xóa (ví dụ: xóa payment có CreatedAt nhỏ nhất)
                    var paymentToDelete = payments.First();

                    // Gọi service để xóa payment
                    await _paymentService.DeletePaymentById(paymentToDelete.Id);

                    return Ok(new { message = "Payment deleted successfully" });
                }
                else
                {
                    return BadRequest(new { message = "Not enough payments to delete" });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        [HttpPost("/getRequestUrl")]
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
                vnpay.AddRequestData("vnp_ReturnUrl", "http://localhost:5173/mentee/payment/ReturnUrl");
                // Tạo URL động bằng cách sử dụng Url.Action
                // var returnUrl = Url.Action("VnpReturnAction", "ControllerName", null, Request.Scheme);

                // vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
                vnpay.AddRequestData("vnp_CurrCode", vnpayConfig.CurrCode);
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(
                    HttpContext
                )); // Ensure Utils.GetIpAddress() is implemented
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.AddHours(7).ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_TxnRef", payment.MenteeApplicattion);
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
