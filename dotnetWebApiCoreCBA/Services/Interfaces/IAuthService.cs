using dotnetWebApiCoreCBA.Models.DTOs.Auth;

namespace dotnetWebApiCoreCBA.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<LoginResponse?> RegisterAsync(RegisterRequest request);

}
