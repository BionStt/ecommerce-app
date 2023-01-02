using Blazored.LocalStorage;
using Ecommerce.Shared;

namespace Ecommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public CartService(ILocalStorageService localStorage, HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _localStorage = localStorage;
        _http = http;
        _authStateProvider = authStateProvider;
    }
    
    public event Action OnChange;

    public async Task AddToCart(CartItem cartItem)
    {
        if ((await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated)
        {
            Console.WriteLine("Auth true");
        }
        else
        {
            Console.WriteLine("Auth false");
        }
        
        List<CartItem>? cart = await GetCartFromLocalStorage();

        var sameItem = cart.Find(p => p.ProductId == cartItem.ProductId && p.ProductTypeId == cartItem.ProductTypeId);
        if (sameItem == null)
        {
        cart.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

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
    public async Task<List<CartProductResponse>> GetCartProducts()
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
        return cartProducts.Data;
    }

    public async Task RemoveProductFromCard(int productId, int productTypeId)
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cartItems == null)
        {
            return;
        }

        var cartItem = cartItems.Find(p => p.ProductId == productId && p.ProductTypeId == productTypeId);
        if (cartItem != null)
        {
            cartItems.Remove(cartItem);
            await _localStorage.SetItemAsync("cart", cartItems);
            OnChange.Invoke();
        }
    }

    public async Task UpdateQuantity(CartProductResponse product)
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cartItems == null)
        {
            return;
        }

        var cartItem = cartItems.Find(p => p.ProductId == product.ProductId && p.ProductTypeId == product.ProductTypeId);
        if (cartItem != null)
        {
            cartItem.Quantity = product.Quantity;
            await _localStorage.SetItemAsync("cart", cartItems);
        }
    }
}
