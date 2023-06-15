namespace WM.Core.Application.Inventories;

public class FilterOptionsDto
{
    public List<FacilityOption> FacilityOptions { get; set; }
    public List<WarehouseOption> WarehouseOptions { get; set; }
    public List<ProductOption> ProductOptions { get; set; }
}

public class FacilityOption
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class WarehouseOption
{
    public int Id { get; set; }
    public int FacilityId { get; set; }
    public string Name { get; set; }
}

public class ProductOption
{
    public int Id { get; set; }
    public string Name { get; set; }
}
