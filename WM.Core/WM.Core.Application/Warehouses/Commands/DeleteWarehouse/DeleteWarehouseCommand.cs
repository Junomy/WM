using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Warehouses.Commands.DeleteWarehouse;

public class DeleteWarehouseCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand>
{
    private readonly IApplicationContext _context;

    public DeleteWarehouseCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if(warehouse != null)
        {
            _context.Warehouses.Remove(warehouse);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
