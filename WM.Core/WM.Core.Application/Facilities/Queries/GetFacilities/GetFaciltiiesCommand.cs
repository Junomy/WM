using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Facilities.Queries.GetFacilities;

public class GetFaciltiiesCommand : IRequest<List<FacilityDto>>
{
    
}

public class GetFaciltiiesCommandHandler : IRequestHandler<GetFaciltiiesCommand, List<FacilityDto>>
{
    private readonly IApplicationContext _context;

    public GetFaciltiiesCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<FacilityDto>> Handle(GetFaciltiiesCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Facilities
            .Include(x => x.Warehouses)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result.Select(x => new FacilityDto 
        { 
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Address = x.Address,
            IsActive = x.IsActive,
            Warehouses = x.Warehouses.Select(x => new Warehouses.WarehouseDto
            { 
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FacilityId = x.FacilityId,
            }).ToList(),
        }).ToList();
    }
}
