namespace Ecommerce.Client.Services.CartService;

public interface ICartService
{
    event Action OnChange;
    Task AddToCart(CartItem cartItem);
    Task<List<CartItem>> GetCartItems();
    Task<List<CartProductResponse>> GetCartProducts();
    Task RemoveProductFromCard(int productId, int productTypeId);
    Task UpdateQuantity(CartProductResponse product);
}
