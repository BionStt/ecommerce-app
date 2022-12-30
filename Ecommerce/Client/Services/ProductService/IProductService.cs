namespace Ecommerce.Client.Services.ProductService;

public interface IProductService
{
    event Action ProductsChanged;
    List<Product> products { get; set; }
    string message { get; set; }
    int currentPage { get; set; }
    int pageCount { get; set; }
    string lastSearchText { get; set; }
    Task GetProducts(string? categoryUrl = null);
    Task<ServiceResponse<Product>> GetProduct(int productId);
    Task SearchProducts(string searchText, int page);
    Task<List<string>> GetProductSearchSuggestion(string searchText);
}
