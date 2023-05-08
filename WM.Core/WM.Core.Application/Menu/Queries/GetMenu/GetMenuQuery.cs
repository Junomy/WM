﻿using MediatR;
using Microsoft.AspNetCore.Http;
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

        return await _context.MenuItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
