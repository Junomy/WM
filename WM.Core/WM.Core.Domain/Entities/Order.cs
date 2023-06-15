namespace WM.Core.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public List<OrderItem> Items { get; set; }
    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
    public int StatusId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime? ClosedAt { get; set; }
}
