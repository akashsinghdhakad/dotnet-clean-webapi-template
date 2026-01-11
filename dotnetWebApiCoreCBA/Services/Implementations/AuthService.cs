using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Models.DTOs.Auth;
using dotnetWebApiCoreCBA.Models.Entities;
using dotnetWebApiCoreCBA.Repositories.Interfaces;
using dotnetWebApiCoreCBA.Services.Interfaces;

namespace dotnetWebApiCoreCBA.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;

    public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions, IEmailService emailService)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtOptions.Value;
        _emailService = emailService;
    }

    public async Task<LoginResponse?> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.Username);
        if (existing != null) return null; // username taken

        CreatePasswordHash(request.Password, out var hash, out var salt);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = "User"
        };

        user = await _userRepository.CreateAsync(user);

        await _emailService.SendAsync(
           toEmail: request.Username, // if username is email
           subject: "Welcome to MyApp",
           body: $"Hi {request.Username}, your account was created successfully!");


        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Username = user.Username,
            Role = user.Role,
            Token = token
        };
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null) return null;

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Username = user.Username,
            Role = user.Role,
            Token = token
        };
    }

    #region Password Hashing (PBKDF2)

    private static void CreatePasswordHash(string password, out string hash, out string salt)
    {
        using var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[16];
        rng.GetBytes(saltBytes);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(32);

        hash = Convert.ToBase64String(hashBytes);
        salt = Convert.ToBase64String(saltBytes);
    }

    private static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashBytes = Convert.FromBase64String(storedHash);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
        var computedHash = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(computedHash, hashBytes);
    }

    #endregion

    #region JWT

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion
}
