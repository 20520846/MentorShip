using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/payment")]

    public class PaymentInfor
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly IConfiguration _configuration;
        public PaymentController(PaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
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