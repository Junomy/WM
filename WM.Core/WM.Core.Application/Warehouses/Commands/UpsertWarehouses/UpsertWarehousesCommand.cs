using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Warehouses.Commands.UpsertWarehouses;

public class UpsertWarehousesCommand : IRequest
{
    public List<WarehouseDto> Warehouses { get; set; }
}

public class UpsertWarehousesCommandHandler : IRequestHandler<UpsertWarehousesCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UpsertWarehousesCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpsertWarehousesCommand request, CancellationToken cancellationToken)
    {
        var inserts = request.Warehouses.Where(x => x.Id == null);
        var updates = request.Warehouses.Where(x => x.Id != null);

        if(inserts.Any())
        {
            await _context.Warehouses
                .AddRangeAsync(await inserts
                    .AsQueryable()
                    .ProjectToListAsync<Warehouse>(_mapper.ConfigurationProvider, cancellationToken));
        }

        if (updates.Any())
        {
            var warehouses = _context.Warehouses.Where(x => updates.Select(x => x.Id).Contains(x.Id));
            await warehouses.ForEachAsync(x =>
            {
                var update = updates.FirstOrDefault(y => y.Id == x.Id);
                x.Name = update.Name;
                x.Description = update.Description;
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return;
    }
}
