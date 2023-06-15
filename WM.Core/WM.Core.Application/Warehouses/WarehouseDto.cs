using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Warehouses;

public class WarehouseDto : IMapFrom<Warehouse>
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int FacilityId { get; set; }
    public string FacilityName { get; set; }
}
