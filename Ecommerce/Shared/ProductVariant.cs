using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce.Shared;

public class ProductVariant
{
    // ProductId + ProductTypeId create a Composite Key

    // JsonIgnore prevents a circular reference because 
    // Product will not be filled when a ProductVariant will be recived  
    [JsonIgnore]
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }

    [Column(TypeName ="decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }
}

