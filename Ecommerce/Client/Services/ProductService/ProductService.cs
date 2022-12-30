using static System.Net.WebRequestMethods;

namespace Ecommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }
    
    public List<Product> products { get; set; } = new();
    public string message { get; set; } = "Loading products...";

    public event Action ProductsChanged;
    public int currentPage { get; set; } = 1;
    public int pageCount { get; set; } = 0;
    public string lastSearchText { get; set; } = string.Empty;

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
        return result;
    }

    public async Task GetProducts(string? categoryUrl = null)
    {
        var result = categoryUrl == null ?
            await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
            await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
        if (result != null && result.Data != null)
        {
            products = result.Data;
        }

        currentPage = 1;
        pageCount = 0;

        if (products.Count == 0)
        {
            message = "No products found";
        }

        ProductsChanged.Invoke();
    }

    public async Task<List<string>> GetProductSearchSuggestion(string searchText)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
        return result.Data;
    }

    public async Task SearchProducts(string searchText, int page)
    {
        lastSearchText = searchText;
        var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");
        if (result != null && result.Data != null)
        {
            products = result.Data.Products;
            currentPage = result.Data.CurrentPage;
            pageCount = result.Data.Pages;
        }           
        if (products.Count == 0)
        {
            message = "No products found.";
        }    
        
        ProductsChanged.Invoke();
    }
}
