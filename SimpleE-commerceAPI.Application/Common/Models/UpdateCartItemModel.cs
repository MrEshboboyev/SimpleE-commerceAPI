namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class UpdateCartItemModel
    {
        public int ShoppingCartItemId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
