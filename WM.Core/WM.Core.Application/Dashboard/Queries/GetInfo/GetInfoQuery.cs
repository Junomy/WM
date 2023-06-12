using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Dashboard.Queries.LineChartWidget;
using WM.Core.Application.Orders;

namespace WM.Core.Application.Dashboard.Queries.GetInfo;

public class GetInfoQuery : IRequest<List<InfoItem>>
{
    public int FacilityId { get; set; }
}

public class GetInfoQueryHandler : IRequestHandler<GetInfoQuery, List<InfoItem>>
{
    private readonly IApplicationContext _context;

    public GetInfoQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<InfoItem>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        var result = new List<InfoItem>();

        var orders = await _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Where(x => x.FacilityId == request.FacilityId && x.StatusId == 3)
            .ToListAsync(cancellationToken);

        var items = new List<OrderItemDto>();

        foreach (var order in orders)
        {
            var productids = order.Items.Select(x => x.ProductId).Distinct();

            var invProducts = await _context.Inventories
                .AsNoTracking()
                .Include(x => x.Warehouse)
                .Where(i =>
                    i.Warehouse.FacilityId == order.FacilityId &&
                    productids.Contains(i.ProductId))
                .ToListAsync(cancellationToken);

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
                        Price = invProducts.First(x => x.ProductId == item.ProductId).Price,
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

        result = items.Select(item => new InfoItem
        {
            Id = item.Id,
            Name = item.Name,
            Sum = item.Price * item.Amount,
            Amount = item.Amount
        }).ToList();

        return result;
    }
}
