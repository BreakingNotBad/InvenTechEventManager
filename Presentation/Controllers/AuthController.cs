using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // 1. ตั้งค่า Cookie Options
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // ป้องกัน JavaScript เข้าถึง (กัน XSS)
            Secure = true, // ส่งผ่าน HTTPS เท่านั้น
            SameSite = SameSiteMode.None, // ป้องกัน CSRF
            Expires = DateTime.UtcNow.AddDays(7) // อายุของ Refresh Token
        };

        // 2. ยัด Refresh Token ลงใน Cookie
        Response.Cookies.Append("refreshToken", result.refreshToken, cookieOptions);

        // 3. ส่งเฉพาะ Access Token กลับไปใน JSON Body (เพื่อเก็บไว้ใน Memory ของ React)
        return Ok(new { result.accessToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        // 1. ดึง Refresh Token จาก Cookie ชื่อ "refreshToken"
        var oldRefreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(oldRefreshToken))
            return Unauthorized();

        try
        {
            // 2. เรียก Service ตามปกติ
            var result = await _auth.RefreshAsync(oldRefreshToken);

            // 3. ตั้งค่า Cookie ตัวใหม่กลับไปให้ Browser (Token Rotation)
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // ป้องกัน JavaScript เข้าถึง (กัน XSS)
                Secure = true, // ส่งผ่าน HTTPS เท่านั้น
                SameSite = SameSiteMode.None, // ป้องกัน CSRF
                Expires = DateTime.UtcNow.AddDays(7) // อายุของ Refresh Token
            };

            Response.Cookies.Append("refreshToken", result.refreshToken, cookieOptions);

            // 4. ส่งกลับเฉพาะ Access Token ใน Body (เพราะมันเก็บใน Memory ของ React)
            return Ok(new { result.accessToken });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        // 1. ดึง Refresh Token จากคุกกี้
        var refreshToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            // 2. สั่ง Service ให้ไปแก้ IsRevoked = true ใน DB
            await _auth.LogoutAsync(refreshToken);
        }

        // 3. สั่ง Browser ให้ลบคุกกี้ทิ้ง (Overwrite ด้วยค่าว่างและตั้งให้หมดอายุในอดีต)
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(-1)
        });

        return NoContent();
    }

    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword(SetPasswordDto dto)
    {
        await _auth.SetPasswordAsync(dto);
        return Ok(new { success = true, message = "Set you Password successfully" });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        await _auth.ForgotPasswordAsync(dto);
        return Ok(new { success = true, message = "Send link to your Email" });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordDto dto)
    {
        var staffId =
            int.Parse(User.FindFirst("staffId")!.Value);

        await _auth.ChangePasswordAsync(staffId, dto);
        return Ok(new { success = true, message = "Password changed successfully" });
    }


}
