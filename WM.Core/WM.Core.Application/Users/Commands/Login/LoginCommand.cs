using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WM.Core.Application.Common.Authorization;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Application.Users.Commands.Login;

public class LoginCommand : IRequest<Object?>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Object?>
{
    private readonly IApplicationContext _context;

    public LoginCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Object?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken);
        if (user is not null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("JunomySecretKey13");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = JwtHelper.GetToken(user.Email, user.Role.ToString(), new TimeSpan(1, 0, 0));
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new {
                token = jwtToken,
                expiresAt = DateTime.UtcNow.Add(new TimeSpan(1, 0, 0))
            };
        }
        return null;
    }
}
