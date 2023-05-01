using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Inventories.Commands.DeleteInvItem;

public class DeleteInvItemCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteInvItemCommandHandler : IRequestHandler<DeleteInvItemCommand, int>
{
    private readonly IApplicationContext _context;

    public DeleteInvItemCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteInvItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if(item == null)
        {
            return -1;
        }
        _context.Inventories.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
