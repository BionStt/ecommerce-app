using Stripe;

namespace Ecommerce.Server.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly ICartService _cartService;
    private readonly IAuthService _authService;
    private readonly IOrderService _orderService;

    public PaymentService(IConfiguration configuration, ICartService cartService, IAuthService authService, IOrderService orderService)
    {
        _configuration = configuration;
        _cartService = cartService;
        _authService = authService;
        _orderService = orderService;

        StripeConfiguration.ApiKey = _configuration.GetSection("Stripe:API").Value;
    }
    
    public async Task<Session> CreateCheckoutSession()
    {
        var products = (await _cartService.GetDbCartProducts()).Data;
        var lineItems = new List<SessionLineItemOptions>();
        products.ForEach(p => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = p.Price * 100,
                Currency = "eur",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = p.Title,
                    Images = new List<string> 
                    { 
                        p.ImageUrl 
                    }
                }
            },
            Quantity = p.Quantity
        }));

        var options = new SessionCreateOptions
        {
            CustomerEmail = _authService.GetUserEmail(),
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = _configuration.GetSection("Stripe:SuccessUrl").Value,
            CancelUrl = _configuration.GetSection("Stripe:CancelUrl").Value
        };

        var service = new SessionService();
        Session session = service.Create(options);
        return session;
    }
}
