using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;
using WM.Core.Domain.Enums;

namespace WM.Core.Application.Menu.Queries.GetMenu;

public class GetMenuQuery : IRequest<List<MenuItem>>
{

}

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, List<MenuItem>>
{
    private readonly IApplicationContext _context;
    private readonly IUserService _userService;

    public GetMenuQueryHandler(IApplicationContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<List<MenuItem>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(cancellationToken);

        if(user == null)
        {
            return new List<MenuItem>();
        }
        var menu = _context.MenuItems.AsNoTracking();
        if (user.Role == Roles.Admin)
        {
            menu = menu.Where(x => x.Admin);
        }
        if (user.Role == Roles.Manager)
        {
            menu = menu.Where(x => x.Manager);
        }
        if (user.Role == Roles.Worker)
        {
            menu = menu.Where(x => x.Worker);
        }

        return await menu.ToListAsync(cancellationToken);
    }
}
