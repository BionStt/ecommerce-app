namespace Ecommerce.Server.Services.PaymentService;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSession();
}
