using WM.Core.Domain.Entities;

namespace WM.Core.Application.Inventories;

public class InventoryDto
{
    public int Id { get; set; }
    public double InventoryPrice { get; set; }
    public double Amount { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public double ProductPrice { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public string WarehouseDescription { get; set; }

    public int FacilityId { get; set; }
}
