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
    private readonly IMapper _mapper;

    public GetWarehousesByFacilityIdQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<WarehouseDto>> Handle(GetWarehousesByFacilityIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Warehouses
            .AsNoTracking()
            .Where(x => x.FacilityId == request.FacilityId)
            .ProjectToListAsync<WarehouseDto>(_mapper.ConfigurationProvider, cancellationToken);
    }
}

