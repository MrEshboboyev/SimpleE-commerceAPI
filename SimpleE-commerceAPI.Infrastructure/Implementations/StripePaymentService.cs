using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using Stripe;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class StripePaymentService : IPaymentService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public StripePaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreatePaymentTokenAsync(CreateTokenModel model)
        {
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = model.CardNumber,
                    ExpMonth = model.ExpMonth,
                    ExpYear = model.ExpYear,
                    Cvc = model.Cvc,
                },
            };

            var service = new TokenService();
            Token token = await service.CreateAsync(options);
            return token.Id;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest paymentRequest)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(paymentRequest.Amount * 100), // Amount in cents
                Currency = "usd",
                Description = paymentRequest.Description,
                Source = paymentRequest.Token
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return new PaymentResult
            {
                Success = (charge.Status == "succeeded"),
                TransactionId = charge.Id,
                ErrorMessage = (charge.Status != "succeeded" ? charge.FailureMessage : null)
            };
        }

        // only Admins
        public async Task<Payment> CreatePaymentAsync(CreatePaymentModel model)
        {
            try
            {
                var payment = new Payment
                {
                    Amount = model.Amount,
                    PaymentMethod = model.PaymentMethod,
                    OrderId = model.OrderId,
                    Status = model.Status,
                    PaymentDate = DateTime.UtcNow
                };

                _unitOfWork.Payment.Add(payment);
                _unitOfWork.Save();
                return payment;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return _unitOfWork.Payment.GetAll(includeProperties: "Order");
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return _unitOfWork.Payment.Get(p => p.PaymentId == paymentId, 
                includeProperties: "Order");
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByMethodAsync(string paymentMethod)
        {
            return _unitOfWork.Payment.GetAll(p => p.PaymentMethod == paymentMethod,
                includeProperties: "Order");
        }

        public async Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId)
        {
            List<Payment> payments = new();

            // get user orders
            var orders = _unitOfWork.Order.GetAll(o => o.UserId == userId);

            // get all orders payments
            foreach (var order in orders)
            {
                payments.Add(_unitOfWork.Payment.Get(p => p.OrderId == order.OrderId));
            }

            return payments;
        }

        
        public async Task<bool> RemovePaymentAsync(int paymentId)
        {
            try
            {
                var paymentFromDb = _unitOfWork.Payment.Get(p => p.PaymentId == paymentId);
                _unitOfWork.Payment.Remove(paymentFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Payment> UpdatePaymentAsync(UpdatePaymentModel model)
        {
            try
            {
                var paymentFromDb = _unitOfWork.Payment.Get(p => 
                    p.PaymentId == model.PaymentId);

                paymentFromDb.Amount = model.Amount;
                paymentFromDb.PaymentMethod = model.PaymentMethod;
                paymentFromDb.OrderId = model.OrderId;
                paymentFromDb.Status = model.Status;

                _unitOfWork.Payment.Update(paymentFromDb);
                _unitOfWork.Save();
                return paymentFromDb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
