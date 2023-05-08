using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;

namespace WM.Core.Application.Warehouses.Queries.GetWarehousesByFacility;

public class GetWarehousesByFacilityIdQuery : IRequest<IList<WarehouseDto>>
{
    public int FacilityId { get; set; }
}

public class GetWarehousesByFacilityIdQueryHandler : IRequestHandler<GetWarehousesByFacilityIdQuery, IList<WarehouseDto>>
{
    private readonly IApplicationContext _context;

    public GetWarehousesByFacilityIdQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<WarehouseDto>> Handle(GetWarehousesByFacilityIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Warehouses
            .Include(x => x.Facility)
            .AsNoTracking()
            .Where(x => x.FacilityId == request.FacilityId)
            .Select(x => new WarehouseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FacilityId = x.FacilityId,
                FacilityName = x.Facility.Name
            })
            .ToListAsync(cancellationToken);
    }
}

