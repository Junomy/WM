using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<User?>
{
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserService _userService;

    public GetUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUser(cancellationToken);
    }
}
