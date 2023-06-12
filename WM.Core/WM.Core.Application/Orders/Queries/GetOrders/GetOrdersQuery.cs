using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<List<OrderDto>>
{
    public int? FacilityId { get; set; }
    public string? OrderNumber { get; set; }
    public List<int>? StatusIds { get; set; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IApplicationContext _context;

    public GetOrdersQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Include(x => x.Status)
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);

        if(request.FacilityId != null)
        {
            orders = orders.Where(x => x.FacilityId == request.FacilityId).ToList();
        }
        if (!request.OrderNumber.IsNullOrEmpty())
        {
            orders = orders.Where(x => $"{x.Id}".StartsWith(request.OrderNumber)).ToList();
        }
        if (!request.StatusIds.IsNullOrEmpty())
        {
            orders = orders.Where(x => request.StatusIds.Contains(x.StatusId)).ToList();
        }

        var result = new List<OrderDto>();

        if (!orders.Any()) return result;

        var productids = orders.SelectMany(x => x.Items.Select(x => x.ProductId)).Distinct();
        var invProducts = await _context.Inventories
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .Where(i =>
                i.Warehouse.FacilityId == orders.First().FacilityId && 
                productids.Contains(i.ProductId))
            .ToListAsync(cancellationToken);
        foreach(var order in orders)
        {
            var items = new List<OrderItemDto>();
            foreach(var item in order.Items)
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
            result.Add(new OrderDto
            {
                Id = order.Id,
                Sum = items.Select(x => x.Price * x.Amount).Sum(),
                StatusId = order.StatusId,
                Status = order.Status.Name,
                FacilityId = order.FacilityId,
                FacilityName = order.Facility.Name,
                Items = items
            });
        }

        return result;
    }
}