using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MinimalEndpoints.Services;

public class TokenService
{
    public string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b4f2e725b7a04e658953ea0d8be670e95a4cfe67e8a94f2c9617c2761e26d8f5"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.Name, username),
                new Claim("idade", "25"), // exemplo de claim customizada
                new Claim(ClaimTypes.Role, "Admin") // exemplo de role
            };

        var token = new JwtSecurityToken(
            issuer: "MinimalApi",
            audience: "MinimalApi",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
