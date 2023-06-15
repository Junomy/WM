using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Warehouses.Queries.GetWarehouse;

public class GetWarehouseQuery : IRequest<WarehouseDto?>
{
    public int Id { get; set; }
}

public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, WarehouseDto?>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetWarehouseQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WarehouseDto?> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        return await _context.Warehouses
            .AsNoTracking()
            .ProjectTo<WarehouseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
