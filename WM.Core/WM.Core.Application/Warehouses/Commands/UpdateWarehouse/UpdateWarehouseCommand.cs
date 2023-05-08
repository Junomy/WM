using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand>
{
    private readonly IApplicationContext _context;

    public UpdateWarehouseCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if(warehouse != null)
        {
            warehouse.Name = request.Name;
            warehouse.Description = request.Description;
        }
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
