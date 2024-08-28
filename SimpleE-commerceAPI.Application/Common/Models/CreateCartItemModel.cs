namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class CreateCartItemModel
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
