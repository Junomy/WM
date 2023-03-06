using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Facilities;

public class FacilityDto : IMapFrom<Facility>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}
