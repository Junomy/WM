namespace WM.Core.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public double Price { get; set; }
        public double Amount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
