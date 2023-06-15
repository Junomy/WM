using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Orders.Queries.GetOrder;

public class GetOrderQuery : IRequest<OrderDto?>
{
    public int Id { get; set; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IApplicationContext _context;

    public GetOrderQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Include(x => x.Facility)
            .Include(x => x.Status)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if(order == null)
        {
            return null;
        }

        var productids = order.Items.Select(x => x.ProductId).Distinct();
        var invProducts = await _context.Inventories
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .Where(i =>
                i.Warehouse.FacilityId == order.FacilityId &&
                productids.Contains(i.ProductId))
            .ToListAsync(cancellationToken);

        var items = new List<OrderItemDto>();

        foreach (var item in order.Items)
        {
            items.Add(new OrderItemDto
            {
                Id = item.Id,
                Name = item.Product.Name,
                Amount = item.Amount,
                Price = invProducts.First(x => x.ProductId == item.ProductId).Price,
                OrderId = order.Id,
                ProductId = item.ProductId,
            });
        }
        var result = new OrderDto
        {
            Id = order.Id,
            Sum = items.Select(x => x.Price * x.Amount).Sum(),
            StatusId = order.StatusId,
            Status = order.Status.Name,
            FacilityId = order.FacilityId,
            FacilityName = order.Facility.Name,
            Items = items
        };

        return result;
    }
}
