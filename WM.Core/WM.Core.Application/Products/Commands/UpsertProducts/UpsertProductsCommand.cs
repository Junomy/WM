using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Products.Commands.UpsertProducts;

public class UpsertProductsCommand : IRequest
{
    public List<ProductDto> Products { get; set; }
}

public class UpsertProductsCommandHandler : IRequestHandler<UpsertProductsCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UpsertProductsCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpsertProductsCommand request, CancellationToken cancellationToken)
    {
        var inserts = request.Products.Where(x => x.Id == null);
        var updates = request.Products.Where(x => x.Id != null);

        if(inserts.Any())
        {
            await _context.Products
                .AddRangeAsync(await inserts
                    .AsQueryable()
                    .ProjectToListAsync<Product>(_mapper.ConfigurationProvider, cancellationToken));
        }

        if (updates.Any())
        {
            var products = _context.Products.Where(x => updates.Select(x => x.Id).Contains(x.Id));
            await products.ForEachAsync(x =>
            {
                var update = updates.FirstOrDefault(y => y.Id == x.Id);
                x.Name = update.Name;
                x.Description = update.Description;
                x.Price = update.Price;
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return;
    }
}
