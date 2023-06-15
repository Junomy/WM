using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Facilities.Commands.UpsertFacility;

public class UpsertFacilityCommand : IRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public bool? IsActive { get; set; }
}

public class UpsertFacilityCommandHandler : IRequestHandler<UpsertFacilityCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public UpsertFacilityCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpsertFacilityCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            var facility = await _context.Facilities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (facility is not null)
            {
                facility.Name = request.Name;
                facility.Description = request.Description;
                facility.Address = request.Address;
                facility.IsActive = request.IsActive ?? true;
            }
        }
        else
        {
            var facility = new Facility
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                IsActive = request.IsActive ?? true
            };

            await _context.Facilities.AddAsync(facility, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
