using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Inventories.Queries.GetFilterOptions;

public class GetFilterOptionsQuery : IRequest<FilterOptionsDto>
{
}

public class GetFilterOptionsQueryHandler : IRequestHandler<GetFilterOptionsQuery, FilterOptionsDto>
{
    private readonly IApplicationContext _context;

    public GetFilterOptionsQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<FilterOptionsDto> Handle(GetFilterOptionsQuery request, CancellationToken cancellationToken)
    {
        FilterOptionsDto options = new FilterOptionsDto();
        options.FacilityOptions = _context.Facilities
            .AsNoTracking()
            .Select(x => new FacilityOption
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();

        options.WarehouseOptions = _context.Warehouses
            .AsNoTracking()
            .Select(x => new WarehouseOption
            {
                Id = x.Id,
                FacilityId = x.FacilityId,
                Name = x.Name,
            })
            .ToList();
        options.ProductOptions = _context.Products
            .AsNoTracking()
            .Select(x => new ProductOption
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();

        return options;
    }
}