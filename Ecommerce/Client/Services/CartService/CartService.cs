using Blazored.LocalStorage;

namespace Ecommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;

    public CartService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    
    public event Action OnChange;

    public async Task AddToCart(CartItem cartItem)
    {
        List<CartItem>? cart = await GetCartFromLocalStorage();
        cart.Add(cartItem);

        await _localStorage.SetItemAsync("cart", cart);
        OnChange?.Invoke();
    }


    public async Task<List<CartItem>> GetCartItems()
    {
        List<CartItem>? cart = await GetCartFromLocalStorage();

        return cart;
    }

    private async Task<List<CartItem>> GetCartFromLocalStorage()
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart == null)
        {
            cart = new();
        }

        return cart;
    }
}
