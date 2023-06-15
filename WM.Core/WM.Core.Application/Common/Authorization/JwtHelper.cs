using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WM.Core.Application.Common.Authorization;

public class JwtHelper
{
    private static string uniqueKey = "JunomySecretKey13";
    private static string issuer = "http://localhost:4200";
    private static string audience = "http://localhost:4200";
    public static JwtSecurityToken GetToken(
        string email,
        string role,
        TimeSpan expiration)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(uniqueKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.Add(expiration),
            claims: claims,
            signingCredentials: creds);
    }
}
