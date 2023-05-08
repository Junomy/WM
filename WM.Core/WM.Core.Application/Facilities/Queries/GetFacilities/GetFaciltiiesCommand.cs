using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;

namespace WM.Core.Application.Facilities.Queries.GetFacilities;

public class GetFaciltiiesCommand : IRequest<List<FacilityDto>>
{
    
}

public class GetFaciltiiesCommandHandler : IRequestHandler<GetFaciltiiesCommand, List<FacilityDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetFaciltiiesCommandHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FacilityDto>> Handle(GetFaciltiiesCommand request, CancellationToken cancellationToken)
    {
        return await _context.Facilities
            .AsNoTracking()
            .ProjectToListAsync<FacilityDto>(_mapper.ConfigurationProvider, cancellationToken);
    }
}
