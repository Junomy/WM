namespace WM.Core.Domain.Entities;

public class Facility : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public List<Warehouse> Warehouses { get; set; }
    public List<User> Users { get; set; }
    public List<Order> Orders { get; set; }
}
