using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IApplicationContext _context;

    public DeleteProductCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        if(product == null) { return -1; }
        product.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
