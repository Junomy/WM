using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Orders;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Dashboard.Queries.DonutWidget;

public class DonutWidgetQuery : IRequest<List<ProductDonutModel>>
{
    public int FacilityId { get; set; }
}

public class DonutWidgetQueryHandler : IRequestHandler<DonutWidgetQuery, List<ProductDonutModel>>
{
    private readonly IApplicationContext _context;

    public DonutWidgetQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDonutModel>> Handle(DonutWidgetQuery request, CancellationToken cancellationToken)
    {
        var result = new List<ProductDonutModel>();

        var orders = await _context.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Where(x => x.FacilityId == request.FacilityId && x.StatusId == 3)
            .ToListAsync(cancellationToken);

        var items = new List<OrderItemDto>();

        foreach (var order in orders) {
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
                if(items.Any(x => x.ProductId == item.ProductId)) {
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
        var sum = items.Select(x => x.Amount * x.Price).Sum();

        foreach (var item in items)
        {
            result.Add(new ProductDonutModel
            {
                Id = item.ProductId,
                Name = item.Name,
                Percentage = ((item.Price * item.Amount) / sum) * 100,
            });
        }

        return result;
    }
}
