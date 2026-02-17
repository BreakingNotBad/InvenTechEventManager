using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts.DTOs;
using Service.Contracts.DTOs.Auth;
using Service.Contracts.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repo;
    private readonly IConfiguration _config;
    private readonly IEmailService _emailService;

    public AuthService(IRepositoryManager repo, IConfiguration config, IEmailService emailService)
    {
        _repo = repo;
        _config = config;
        _emailService = emailService;
    }

    // LOGIN
    public async Task<(string accessToken, string refreshToken)>
        LoginAsync(string email, string password)
    {
        var staff = await _repo.Staff.GetStaffForLoginAsync(email);

        if (staff == null || staff.Password != password)
            throw new UnauthorizedException("Invalid credentials");

        var accessToken = GenerateJwt(staff);

        var refresh = new RefreshToken
        {
            StaffId = staff.StaffId,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _repo.RefreshToken.CreateAsync(refresh);
        await _repo.SaveAsync();

        return (accessToken, refresh.Token);
    }

    // REFRESH
    public async Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
    {
        // ค้นหา RefreshToken และ Include ข้อมูล Staff มาด้วยเพื่อใช้ปั๊ม JWT
        var oldRefresh = await _repo.RefreshToken.GetByTokenAsync(refreshToken);

        // เช็คว่า RefreshToken มีจริงไหม / หมดอายุหรือยัง / ถูกยกเลิกไปหรือยัง
        if (oldRefresh?.IsRevoked != false || oldRefresh.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Invalid or expired refresh token");

        // ทำลาย RefreshToken ตัวเก่าทิ้ง (เพื่อความปลอดภัย)
        oldRefresh.IsRevoked = true;
        await _repo.RefreshToken.UpdateAsync(oldRefresh);

        // สร้าง RefreshToken ตัวใหม่
        var newRefresh = new RefreshToken
        {
            StaffId = oldRefresh.StaffId,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _repo.RefreshToken.CreateAsync(newRefresh);
        await _repo.SaveAsync();

        // สร้าง Access Token ใหม่โดยใช้ข้อมูล Staff
        var accessToken = GenerateJwt(oldRefresh.Staff);

        return (accessToken, newRefresh.Token);
    }

    // LOGOUT
    public async Task LogoutAsync(string refreshToken)
    {
        var token = await _repo.RefreshToken.GetByTokenAsync(refreshToken);
        if (token == null) return;

        token.IsRevoked = true;
        token.ExpiresAt = DateTime.UtcNow;
        await _repo.RefreshToken.UpdateAsync(token);
        await _repo.SaveAsync();
    }

    // SET PASSWORD
    public async Task SetPasswordAsync(SetPasswordDto dto)
    {
        var staff =
            await _repo.Staff.GetByResetTokenAsync(dto.Token);

        if (staff == null)
            throw new Exception("Invalid token");

        if (staff.PasswordResetTokenExpire < DateTime.UtcNow)
            throw new Exception("Token expired");

        staff.Password = dto.NewPassword;

        staff.PasswordResetToken = null;
        staff.PasswordResetTokenExpire = null;

        await _repo.SaveAsync();
    }

    // FORGOT PASSWORD
    public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var staff = await _repo.Staff.GetByEmailAsync(dto.Email);
        if (staff == null) return;

        var token = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

        staff.PasswordResetToken = token;
        staff.PasswordResetTokenExpire = DateTime.UtcNow.AddHours(1);

        await _repo.SaveAsync();

        var link =
          $"http://localhost:5173/forget-password?token={token}";

        await _emailService.SendAsync(
            staff.Email,
            "Reset password",
            $"Click link to reset password:\n{link}"
        );
    }

    // CHANGE PASSWORD
    public async Task ChangePasswordAsync(
    int staffId,
    ChangePasswordDto dto)
    {
        var staff = await _repo.Staff.GetStaffByIdAsync(staffId, true);

        if (staff == null)
            throw new Exception("Staff not found");

        if (staff.Password != dto.CurrentPassword)
            throw new Exception("Current password incorrect");

        staff.Password = dto.NewPassword;

        await _repo.SaveAsync();
    }

    // JWT
    private string GenerateJwt(Staff staff)
    {
        var claims = new List<Claim>
        {
            new Claim("staffId", staff.StaffId.ToString()),
            new Claim("fullName", staff.FullName),
            new Claim("email", staff.Email),
            new Claim("avatar", staff.Avatar ?? "")
        };

        foreach (var r in staff.StaffRoles)
            claims.Add(new Claim("Role", r.Role.RoleName));

        foreach (var p in staff.StaffPermissions)
            claims.Add(new Claim("permission", p.Permission.PermissionName));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_config["JwtSettings:ExpireMinutes"]!)
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64)
        );
    }
}
