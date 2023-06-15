using WM.Core.Domain.Entities;

namespace WM.Core.Application.Inventories;

public class CreateInventoryDto
{
    public double Price { get; set; }
    public double Amount { get; set; }

    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
}
