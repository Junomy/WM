namespace WM.Core.Application.Inventories;

public class InventoryFilterDto
{
    public List<int> Facilities { get; set; }
    public List<int> Warehouses { get; set; }
    public List<int> Products { get; set; }
    public int MinAmount { get; set; }
    public int MaxAmount { get; set; }
    public int MinSellPrice { get; set; }
    public int MaxSellPrice { get; set; }
    public int MinBuyPrice { get; set; }
    public int MaxBuyPrice { get; set; }
}
