using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs;
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

    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword(SetPasswordDto dto)
    {
        await _auth.SetPasswordAsync(dto);
        return Ok();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        await _auth.ForgotPasswordAsync(dto);
        return Ok();
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordDto dto)
    {
        var staffId =
            int.Parse(User.FindFirst("StaffId")!.Value);

        await _auth.ChangePasswordAsync(staffId, dto);
        return Ok();
    }


}
