using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;

namespace WM.Core.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<List<ProductDto>>
{
    public string? Name { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (request.Name is not null)
        {
            products = products.Where(x => x.Name.Contains(request.Name));
        }
        if(request.MinPrice is not null && request.MaxPrice is not null) {
            products = products.Where(x => x.Price >= request.MinPrice && x.Price <= request.MaxPrice);
        }

        return await products.ProjectToListAsync<ProductDto>(_mapper.ConfigurationProvider);
    }
}
