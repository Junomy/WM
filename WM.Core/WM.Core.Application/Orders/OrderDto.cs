namespace WM.Core.Application.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public double Sum { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public int FacilityId { get; set; }
    public string FacilityName { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}
