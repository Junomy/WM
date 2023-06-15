using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Inventories.Queries.GetInventories;

public class GetInventoriesQuery : IRequest<List<InventoryDto>>
{
    public List<int>? Facilities { get; set; }
    public List<int>? Warehouses { get; set; }
    public List<int>? Products { get; set; }
    public int? MinAmount { get; set; }
    public int? MaxAmount { get; set; }
    public int? MinSellPrice { get; set; }
    public int? MaxSellPrice { get; set; }
    public int? MinBuyPrice { get; set; }
    public int? MaxBuyPrice { get; set; }
}

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, List<InventoryDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetInventoriesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<InventoryDto>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        var results = _context.Inventories
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Warehouse)
            .ThenInclude(x => x.Facility)
            .AsQueryable();

        if(request.Facilities is not null && request.Facilities.Any())
        {
            results = results.Where(x => request.Facilities.Contains(x.Warehouse.FacilityId));
        }
        if (request.Warehouses is not null && request.Warehouses.Any())
        {
            results = results.Where(x => request.Warehouses.Contains(x.WarehouseId));
        }
        if (request.Products is not null && request.Products.Any())
        {
            results = results.Where(x => request.Products.Contains(x.ProductId));
        }
        if (request.MinAmount is not null && request.MaxAmount is not null)
        {
            results = results.Where(x => x.Amount >= request.MinAmount && x.Amount <= request.MaxAmount);
        }
        if (request.MinSellPrice is not null && request.MaxSellPrice is not null)
        {
            results = results.Where(x => x.Price >= request.MinSellPrice && x.Price <= request.MaxSellPrice);
        }
        if (request.MinBuyPrice is not null && request.MaxBuyPrice is not null)
        {
            results = results.Where(x => x.Product.Price >= request.MinBuyPrice && x.Product.Price <= request.MaxBuyPrice);
        }

        return await results
            .Select(x => new InventoryDto
            {
                Id = x.Id,
                InventoryPrice = x.Price,
                Amount = x.Amount,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductDescription = x.Product.Description,
                ProductPrice = x.Product.Price,
                WarehouseId = x.WarehouseId,
                WarehouseName = x.Warehouse.Name,
                WarehouseDescription = x.Warehouse.Description,
                FacilityId = x.Warehouse.FacilityId,
                FacilityName = x.Warehouse.Facility.Name,
            })
            .ToListAsync(cancellationToken);
    }
}
