namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class PaymentRequest
    {
        public string Token { get; set; } // Stripe token from Client - Side
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
