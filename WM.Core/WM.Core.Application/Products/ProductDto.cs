using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Products;

public class ProductDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
