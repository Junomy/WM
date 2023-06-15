using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Users.Commands.UpsertUser;

public class UpsertUserCommand : IRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Position { get; set; }

    public int? FacilityId { get; set; }
}

public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UpsertUserCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpsertUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is not null)
            {
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Email = request.Email;
                user.Password = request.Password;
                user.Position = request.Position;
                user.FacilityId = request.FacilityId;
            }
        }
        else
        {
            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                Position = request.Position,
                FacilityId = request.FacilityId
        };

            await _context.Users.AddAsync(user, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
