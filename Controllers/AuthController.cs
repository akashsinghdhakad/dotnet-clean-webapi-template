using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetWebApiCoreCBA.Common;
using DotnetWebApiCoreCBA.Models.DTOs.Auth;
using DotnetWebApiCoreCBA.Services.Interfaces;

namespace DotnetWebApiCoreCBA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<LoginResponse>.Fail("VALIDATION_ERROR", "Invalid login data"));
        }

        var result = await _authService.LoginAsync(request);
        if (result == null)
        {
            return Unauthorized(ApiResponse<LoginResponse>.Fail("INVALID_CREDENTIALS", "Invalid username or password"));
        }

        return Ok(ApiResponse<LoginResponse>.Ok(result, "Login successful"));
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult<ApiResponse<object>> Me()
    {
        var username = User.Identity?.Name ?? "unknown";

        return Ok(ApiResponse<object>.Ok(new
        {
            Username = username,
            Claims = User.Claims.Select(c => new { c.Type, c.Value })
        }));
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public IActionResult Public()
    {
        return Ok("This is public");
    }

}
