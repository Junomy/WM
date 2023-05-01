using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Products.Commands.UpsertProducts;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.Now,
            CreatedBy = 1,
            ModifiedAt = DateTime.Now,
            ModifiedBy = 1,
            IsDeleted = false
        };
        await _context.Products.AddRangeAsync(product);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProductDto>(product);
    }
}
