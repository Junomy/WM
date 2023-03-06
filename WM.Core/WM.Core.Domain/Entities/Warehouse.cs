namespace WM.Core.Domain.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
    public List<Inventory> Inventories { get; set; }
}
