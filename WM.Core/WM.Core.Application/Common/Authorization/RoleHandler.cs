using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WM.Core.Application.Common.Authorization;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        //if(context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.Role.ToString()))
        //{
        //    context.Succeed(requirement);
        //}

        return Task.CompletedTask;
    }
}
