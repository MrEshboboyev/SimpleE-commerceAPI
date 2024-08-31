namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class CreatePaymentModel
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
