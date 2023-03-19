using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Menu.Queries.GetMenu;

public class GetMenuQuery : IRequest<List<MenuItem>>
{

}

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, List<MenuItem>>
{
    private readonly IApplicationContext _context;

    public GetMenuQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<MenuItem>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        return await _context.MenuItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
