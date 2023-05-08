using Microsoft.AspNetCore.Authorization;
using WM.Core.Domain.Enums;

namespace WM.Core.Application.Common.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public Roles Role { get; }
    public RoleRequirement(Roles role)
    {
        Role = role;
    }
}
