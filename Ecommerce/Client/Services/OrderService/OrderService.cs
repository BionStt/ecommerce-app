using Microsoft.AspNetCore.Components;

namespace Ecommerce.Client.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navManager;

    public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navManager)
    {
        _http = http;
        _authStateProvider = authStateProvider;
        _navManager = navManager;
    }

    private async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }

    public async Task PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            await _http.PostAsync("api/order", null);
        }
        else
        {
            _navManager.NavigateTo("login");
        }
    }
}
