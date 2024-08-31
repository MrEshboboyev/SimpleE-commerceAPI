namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class CreateTokenModel
    {
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string Cvc { get; set; }
    }
}
