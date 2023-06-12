using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Orders;

namespace WM.Core.Application.Dashboard.Queries.LineChartWidget;

public class LineChartWidgetQuery : IRequest<List<ProductChartItem>>
{
    public int FacilityId { get; set; }
}

public class LineChartWidgetQueryHandler : IRequestHandler<LineChartWidgetQuery, List<ProductChartItem>>
{
    private readonly IApplicationContext _context;

    public LineChartWidgetQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<ProductChartItem>> Handle(LineChartWidgetQuery request, CancellationToken cancellationToken)
    {
        var result = new List<ProductChartItem>();

        var orders = await _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Where(x => x.FacilityId == request.FacilityId && x.StatusId == 3)
            .ToListAsync(cancellationToken);

        var items = new List<OrderItemDto>();

        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                if (items.Any(x => x.ProductId == item.ProductId))
                {
                    items.First(x => x.ProductId == item.ProductId).Amount += item.Amount;
                }
                else
                {
                    items.Add(new OrderItemDto
                    {
                        Id = item.Id,
                        Name = item.Product.Name,
                        Amount = item.Amount,
                        ProductId = item.ProductId,
                    });
                }
            }
        }

        items = items.OrderByDescending(x => x.Amount * x.Price).Take(10).ToList();

        orders = orders
            .Where(x => items.Select(x => x.ProductId).Intersect(x.Items.Select(x => x.ProductId)).Any())
            .ToList();

        foreach(var item in items)
        {
            var resItems = orders
                .Where(x => x.Items.Select(x => x.ProductId).Contains(item.ProductId))
                .OrderBy(x => x.ClosedAt)
                .Select(x => new ProductValueItem
                {
                    Amount = x.Items.First(x => x.ProductId == item.ProductId).Amount,
                    SellDate = x.ClosedAt.Value,
                })
                .ToList();

            result.Add(new ProductChartItem
            {
                Id = item.ProductId,
                Name = item.Name,
                Items = resItems,
            });
        }

        return result;
    }
}
