using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IApplicationContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IApplicationContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User?> GetUser(CancellationToken cancellationToken = default)
    {
        var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        if(authHeader.StartsWith("Bearer "))
        {
            User? user = null;
            var jwtToken = authHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userEmail = token.Claims.FirstOrDefault(c => c.Type == "email");

            if (userEmail != null)
            {
                user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail.Value, cancellationToken);
            }

            if(user is not null)
            {
                return user;
            }
        }
        return null;
    }
}
