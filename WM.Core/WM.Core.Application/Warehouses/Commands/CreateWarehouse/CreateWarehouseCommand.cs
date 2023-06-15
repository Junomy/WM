using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int FacilityId { get; set; }
}

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public CreateWarehouseCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        await _context.Warehouses.AddAsync(new Warehouse
        {
            Name = request.Name,
            Description = request.Description,
            FacilityId = request.FacilityId,
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return;
    }
}
