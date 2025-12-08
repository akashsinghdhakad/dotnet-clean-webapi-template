using DotnetWebApiCoreCBA.Models.DTOs.Auth;

namespace DotnetWebApiCoreCBA.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
