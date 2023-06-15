namespace WM.Core.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public List<Inventory> Inventories { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
