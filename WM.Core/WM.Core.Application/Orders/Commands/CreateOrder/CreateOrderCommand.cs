using MediatR;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public int FacilityId { get; set; }
    public int StatusId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IApplicationContext _context;

    public CreateOrderCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            FacilityId = request.FacilityId,
            StatusId = request.StatusId,
            Items = request.Items.Select(x => new OrderItem
            {
                Amount = x.Amount,
                ProductId = x.ProductId,
            }).ToList(),
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new OrderDto
        {
            Id = order.Id,
            StatusId = order.StatusId,
            FacilityId = order.FacilityId,
        };
    }
}
