using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Inventories.Queries.GetInventories;

public class GetInventoriesQuery : IRequest<List<InventoryDto>>
{
    public List<int> Ids { get; set; }
}

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, List<InventoryDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public async Task<List<InventoryDto>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Warehouse)
            .Where(x => request.Ids.Contains(x.Id))
            .Select(x => new InventoryDto
            {
                Id = x.Id,
                InventoryPrice = x.Price,
                Amount = x.Amount,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductDescription = x.Product.Description,
                WarehouseId = x.WarehouseId,
                WarehouseName = x.Warehouse.Name,
                WarehouseDescription = x.Warehouse.Description,
            })
            .ToListAsync(cancellationToken);
    }
}
