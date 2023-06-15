using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Inventories.Commands.CreateInvItems;

public class CreateInvItemsCommand : IRequest
{
    public int? Id { get; set; }
    public double Price { get; set; }
    public double Amount { get; set; }

    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
}

public class CreateInvItemsCommandHandler : IRequestHandler<CreateInvItemsCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public CreateInvItemsCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(CreateInvItemsCommand request, CancellationToken cancellationToken)
    {
        if(request.Id.HasValue)
        {
            var item = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item != null)
            {
                item.Price = request.Price;
                item.Amount = request.Amount;
                item.WarehouseId = request.WarehouseId;
                item.ProductId = request.ProductId;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        else
        {
            var item = await _context.Inventories
                .FirstOrDefaultAsync(x => x.WarehouseId == request.WarehouseId && x.ProductId == x.ProductId, cancellationToken);
            if (item != null)
            {
                item.Price = request.Price;
                item.Amount += request.Amount;
                item.WarehouseId = request.WarehouseId;
                item.ProductId = request.ProductId;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else 
            {
                await _context.Inventories.AddAsync(new Inventory
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    Amount = request.Amount,
                    Price = request.Price,
                }, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        return;
    }
}
