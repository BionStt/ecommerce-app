using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ecommerce.Client;

public static class RegisterServices
{
    public static void ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<IProductService, ProductService>();
    }
}
