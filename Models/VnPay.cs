namespace MentorShip.Models
{
    public class VnPay
    {
        public string vnp_Version { get; set; }
        public string vnp_Command { get; set; }
        public string vnp_CurrCode { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_Amount { get; set; }
        public string vnp_Locale { get; set; }
        public string vnp_SecureHash { get; set; }
    }
}