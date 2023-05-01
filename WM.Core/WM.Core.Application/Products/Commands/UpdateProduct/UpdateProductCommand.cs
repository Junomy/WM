using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Products.Commands.UpsertProducts;
using WM.Core.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WM.Core.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductDto?>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
{

    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == request.Id);
        if (product == null) return null;
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProductDto>(product);
    }
}
