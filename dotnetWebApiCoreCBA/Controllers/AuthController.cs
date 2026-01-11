using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Models.DTOs.Auth;
using dotnetWebApiCoreCBA.Services.Interfaces;

namespace dotnetWebApiCoreCBA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<LoginResponse?>.Fail("VALIDATION_ERROR","Invalid data"));

        var result = await _authService.RegisterAsync(request);
        if (result == null)
            return BadRequest(ApiResponse<LoginResponse?>.Fail("ERR_USER_EXISTS","Username already exists"));

        return Ok(ApiResponse<LoginResponse>.Ok(result, "User registered successfully"));
    }

    // POST: api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<LoginResponse?>.Fail("BADREQUEST","Invalid data"));

        var result = await _authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(ApiResponse<LoginResponse?>.Fail("UNAUTHORIZED","Invalid username or password"));

        return Ok(ApiResponse<LoginResponse>.Ok(result, "Login successful"));
    }

    // GET: api/auth/me
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var username =
            User.Identity?.Name
            ?? User.FindFirst("unique_name")?.Value
            ?? User.FindFirst("unique_name")?.Value;

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        var data = new
        {
            Username = username,
            Role = role
        };

        return Ok(ApiResponse<object>.Ok(data));
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public IActionResult Public()
    {
        return Ok("This is public");
    }

}
