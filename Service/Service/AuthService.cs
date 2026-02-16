using Contracts.IRepository.BaseManager;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repo;
    private readonly IConfiguration _config;

    public AuthService(IRepositoryManager repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    // LOGIN 
    public async Task<(string accessToken, string refreshToken)>
        LoginAsync(string email, string password)
    {
        var staff = await _repo.Staff.GetStaffForLoginAsync(email);

        if (staff == null || staff.Password != password)
            throw new UnauthorizedAccessException("Invalid credentials");

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
    public async Task<(string accessToken, string refreshToken)>
        RefreshAsync(string refreshToken)
    {
        var old = await _repo.RefreshToken.GetByTokenAsync(refreshToken);

        if (old == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        old.IsRevoked = true;
        await _repo.RefreshToken.UpdateAsync(old);
        await _repo.SaveAsync();
        var newRefresh = new RefreshToken
        {
            StaffId = old.StaffId,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _repo.RefreshToken.CreateAsync(newRefresh);
        await _repo.SaveAsync();
        var accessToken = GenerateJwt(old.Staff);

        return (accessToken, newRefresh.Token);
    }

    // LOGOUT 
    public async Task LogoutAsync(string refreshToken)
    {
        var token = await _repo.RefreshToken.GetByTokenAsync(refreshToken);
        if (token == null) return;

        token.IsRevoked = true;
        await _repo.RefreshToken.UpdateAsync(token);
        await _repo.SaveAsync();
    }

    // JWT 
    private string GenerateJwt(Staff staff)
    {
        var claims = new List<Claim>
        {
            new Claim("StaffId", staff.StaffId.ToString()),
            new Claim("FullName", staff.FullName),
            new Claim("Email", staff.Email)
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
