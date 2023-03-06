using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Application.Common.Mappings;

namespace WM.Core.Application.Facilities.Queries.GetFacility;

public class GetFacilityQuery : IRequest<FacilityDto?>
{
    public int Id { get; set; }
}

public class GetFacilityQueryHandler : IRequestHandler<GetFacilityQuery, FacilityDto?>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetFacilityQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FacilityDto?> Handle(GetFacilityQuery request, CancellationToken cancellationToken)
    {
        return await _context.Facilities
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<FacilityDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
