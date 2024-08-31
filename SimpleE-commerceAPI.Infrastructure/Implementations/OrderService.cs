using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class OrderService : IOrderService
    {
        // inject IUnitOfWork, IPaymentService
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order> CreateOrderAsync(OrderRequestModel model)
        {
            await _unitOfWork.BeginTransactionAsync(); // Start the transaction

            try
            {
                // Step 1 : Create the order
                var order = new Order
                {
                    UserId = model.UserId,
                    OrderDate = DateTime.UtcNow,
                    Status = "Pending",
                    OrderItems = new List<OrderItem>()
                };

                // Fetch the cart items from repository 
                var cart = _unitOfWork.Cart.Get(c => c.User.Id == model.UserId);
                var cartItems = _unitOfWork.CartItem.GetAll(item =>
                    item.ShoppingCartId == cart.ShoppingCartId);

                foreach (var cartItem in cartItems)
                {
                    var product = _unitOfWork.Product.Get(p => p.ProductId == cartItem.ProductId);

                    if (product == null || product.StockQuantity < cartItem.Quantity)
                    {
                        throw new Exception($"Product {cartItem.ProductId} is not available or has insufficient stock.");

                    }

                    // Deduct inventory
                    product.StockQuantity -= cartItem.Quantity;
                    _unitOfWork.Product.Update(product);

                    // Add to order items
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = product.Price,
                    });
                }

                order.TotalAmount = order.OrderItems.Sum(item => item.Price * item.Quantity);
                _unitOfWork.Order.Add(order);
                _unitOfWork.Save();

                // Step 2 : Process Payment
                var paymentRequest = new PaymentRequest
                {
                    Token = model.PaymentToken,
                    Amount = order.TotalAmount,
                    Description = $"Order {order.OrderId} payment"
                };

                var paymentResult = await _paymentService.ProcessPaymentAsync(paymentRequest);

                if (!paymentResult.Success)
                {
                    throw new Exception(paymentResult.ErrorMessage);
                }

                // Save Payment Details
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    PaymentDate = DateTime.UtcNow,
                    Amount = order.TotalAmount,
                    PaymentMethod = model.PaymentMethod,
                    Status = "Completed",
                };
                order.Payment = payment;

                _unitOfWork.Payment.Add(payment);
                _unitOfWork.Save();

                // Step 3 : Update order status
                order.Status = "Completed";
                _unitOfWork.Order.Update(order);
                _unitOfWork.Save();

                // Step 4 : Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                return order;
            }
            catch (Exception ex)
            {
                // Rollback the transaction if something went wrong
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception($"Order creation failed: {ex.Message}", ex);
            }
        }
    }
}
