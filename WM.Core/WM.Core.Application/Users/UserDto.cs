using WM.Core.Application.Common.Mappings;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Users;

public class UserDto : IMapFrom<User>
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Position { get; set; }

    public int? FacilityId { get; set; }
}
