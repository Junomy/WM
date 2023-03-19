using AutoMapper;
using MediatR;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Inventories.Commands.CreateInvItems;

public class CreateInvItemsCommand : IRequest
{
    public List<CreateInventoryDto> Items { get; set; }
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
        await _context.Inventories.AddRangeAsync(request.Items.Select(x => new Inventory
        {
            ProductId = x.ProductId,
            WarehouseId = x.WarehouseId,
            Amount = x.Amount,
            Price = x.Price,
        }), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return;
    }
}
