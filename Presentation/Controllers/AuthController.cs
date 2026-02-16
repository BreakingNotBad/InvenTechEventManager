using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Auth;
using Service.Contracts.IService;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _auth.LoginAsync(dto.Email, dto.Password);

        return Ok(new
        {
            accessToken = result.accessToken,
            refreshToken = result.refreshToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto dto)
    {
        var result = await _auth.RefreshAsync(dto.RefreshToken);

        return Ok(new
        {
            accessToken = result.accessToken,
            refreshToken = result.refreshToken
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenDto dto)
    {
        await _auth.LogoutAsync(dto.RefreshToken);
        return Ok();
    }
}
