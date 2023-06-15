using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest
{
    public int OrderId { get; set; }
    public int StatusId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IApplicationContext _context;

    public UpdateOrderCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if(order.StatusId == 2 && request.StatusId == 3) 
        {
            var inventories = await _context.Inventories
                .Include(x => x.Warehouse)
                .Where(x => 
                    request.Items.Select(x => x.ProductId).Contains(x.ProductId) 
                    && order.FacilityId == x.Warehouse.FacilityId)
                .ToListAsync(cancellationToken);

            inventories.ForEach(x =>
            {
                x.Amount -= request.Items.First(y => y.ProductId == x.ProductId).Amount;
            });

            await _context.SaveChangesAsync(cancellationToken);
        }

        order.StatusId = request.StatusId;
        if(order.StatusId == 3)
        {
            order.ClosedAt = DateTime.Now;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
