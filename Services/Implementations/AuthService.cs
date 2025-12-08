using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DotnetWebApiCoreCBA.Common;
using DotnetWebApiCoreCBA.Models.DTOs.Auth;
using DotnetWebApiCoreCBA.Services.Interfaces;

namespace DotnetWebApiCoreCBA.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwt;

    public AuthService(IOptions<JwtSettings> jwtOptions)
    {
        _jwt = jwtOptions.Value;
    }

    // For demo only – replace with DB check later
    private bool ValidateUser(string username, string password)
        => username == "admin" && password == "admin123";

    public Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        if (!ValidateUser(request.Username, request.Password))
            return Task.FromResult<LoginResponse?>(null);

        var key = Encoding.UTF8.GetBytes(_jwt.Key);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.Username),
            new(ClaimTypes.Role, "Admin")
        };

        var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        var response = new LoginResponse
        {
            Token = jwt,
            ExpiresAt = expires,
            Username = request.Username
        };

        return Task.FromResult<LoginResponse?>(response);
    }
}
