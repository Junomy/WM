namespace WM.Core.Application.Dashboard.Queries.LineChartWidget;

public class ProductChartItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductValueItem> Items { get; set; }
}

public class ProductValueItem
{
    public double Amount { get; set; }
    public DateTime SellDate { get; set; }
}
